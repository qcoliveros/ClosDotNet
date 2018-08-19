namespace ClosDotNet.Domain.Workflow
{
    using ClosDotNet.Domain.CodeSet;
    using ClosDotNet.Domain.Task;
    using ClosDotNet.Domain.User;
    using ClosDotNet.Helper;
    using OptimaJet.Workflow.Core.Builder;
    using OptimaJet.Workflow.Core.Bus;
    using OptimaJet.Workflow.Core.Model;
    using OptimaJet.Workflow.Core.Runtime;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Xml.Linq;
    using ProcessStatus = OptimaJet.Workflow.Core.Persistence.ProcessStatus;
    
    public class OptimaJetWorkflowManager : WorkflowBase
    {
        public override IList<IActionTypeVO> RetrieveWorkflowActions(Guid? processId)
        {
            if (processId != null)
            {
                IList<IActionTypeVO> result = new List<IActionTypeVO>();

                var commands = WorkflowInit.Runtime.GetAvailableCommands((processId ?? Guid.Empty), string.Empty);
                foreach (var workflowCommand in commands)
                {
                    if (result.Count(c => c.Code == workflowCommand.CommandName) == 0)
                    {
                        result.Add(new ActionTypeVO()
                        {
                            Code = workflowCommand.CommandName,
                            Description = workflowCommand.LocalizedName
                        });
                    }
                }

                return result;
            }

            return RetrieveDefaultlActions();
        }

        public override ITaskVO ExecuteWorkflow(ITaskVO task, IUserVO nextOwner)
        {
            if (task.WorkflowProcessId == null)
            {
                task.WorkflowProcessId = Guid.NewGuid();
            }

            Guid processId = (task.WorkflowProcessId ?? Guid.Empty);
            CreateWorkflowIfNotExists(processId, task.WorkflowType);

            ProcessDefinition processDefinition = WorkflowInit.Runtime.GetProcessScheme(processId);
  
            WorkflowCommand command = WorkflowInit.Runtime.GetAvailableCommands(
                (task.WorkflowProcessId ?? Guid.Empty), string.Empty)
                        .Where(c => c.CommandName.Trim() == task.TaskAction).FirstOrDefault();

            if (command != null)
            { 
                WorkflowInit.Runtime.ExecuteCommand(command, string.Empty, string.Empty);

                if (nextOwner != null)
                {
                    // Always assume first that it is for Submit.
                    task.CurrentOwner = nextOwner;
                }
                
                var commands = WorkflowInit.Runtime.GetAvailableCommands(processId, string.Empty);
                if (commands == null || commands.Count() == 0)
                {
                    // For end-states.
                    task.TaskStatus = Constants.GetEnumDescription<TaskStatus>(TaskStatus.COMPLETED);
                    task.CurrentOwner = task.Initiator;
                }
                else
                {
                    ActivityDefinition definition = processDefinition.InitialActivity;
                    WorkflowState state = WorkflowInit.Runtime.GetCurrentState(processId);
                    if (definition.State == state.Name)
                    {
                        // For Draft.
                        task.TaskStatus = Constants.GetEnumDescription<TaskStatus>(TaskStatus.IN_PROGRESS);
                        task.CurrentOwner = task.Initiator;
                    }
                }
            }

            task.Status = WorkflowInit.Runtime.GetCurrentState(processId).Name;
            task.WorkflowDefinitionId = processDefinition.Id;

            return task;
        }

        #region Utility
        private void CreateWorkflowIfNotExists(Guid id, string workflowKey)
        {
            if (WorkflowInit.Runtime.IsProcessExists(id))
            {
                return;
            }

            using (var sync = new WorkflowSync(WorkflowInit.Runtime, id))
            {
                WorkflowInit.Runtime.CreateInstance(workflowKey, id);

                sync.StatrtWaitingFor(
                    new List<ProcessStatus> { ProcessStatus.Idled, ProcessStatus.Initialized });
                sync.Wait(new TimeSpan(0, 0, 10));
            }
        }
        #endregion Utility
    }

    public class WorkflowInit
    {
        private static volatile WorkflowRuntime _runtime;
        private static readonly object _sync = new object();
        public static WorkflowRuntime Runtime
        {
            get
            {
                if (_runtime == null)
                {
                    lock (_sync)
                    {
                        if (_runtime == null)
                        {
                            var connectionString =
                                ConfigurationManager.ConnectionStrings["WorkflowConnection"].ConnectionString;
                            var builder = new WorkflowBuilder<XElement>(
                                new OptimaJet.Workflow.DbPersistence.DbXmlWorkflowGenerator(connectionString),
                                new OptimaJet.Workflow.Core.Parser.XmlWorkflowParser(),
                                new OptimaJet.Workflow.DbPersistence.DbSchemePersistenceProvider(connectionString)
                                ).WithDefaultCache();
                            _runtime = new WorkflowRuntime(new Guid("{8D38DB8F-F3D5-4F26-A989-4FDD40F32D9D}"))
                                .WithBuilder(builder)
                                .WithPersistenceProvider(
                                    new OptimaJet.Workflow.DbPersistence.DbPersistenceProvider(connectionString))
                                .WithTimerManager(new TimerManager())
                                .WithBus(new NullBus())
                                .SwitchAutoUpdateSchemeBeforeGetAvailableCommandsOn()
                                .Start();
                        }
                    }
                }

                return _runtime;
            }
        }
    }
}