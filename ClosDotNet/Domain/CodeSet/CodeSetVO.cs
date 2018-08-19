namespace ClosDotNet.Domain.CodeSet
{
    using ClosDotNet.Domain.Base.Model;
    using System;

    public interface ICodeSetVO : IDualControlVO
    {
        string Code { get; set; }

        string Name { get; set; }

        string MaintenanceInd { get; set; }
    }

    [Serializable()]
    public class CodeSetVO : DualControlVO
    {
        public static readonly string[] EXCLUDE_COPY = new string[] 
        {
            "Id",
            "VersionNumber",
            "CreateBy",
            "CreationDate"
        };

        public CodeSetVO() 
            : base ()
        {
        }

        public virtual string Code { get; set; }

        public virtual string Name { get; set; }

        public virtual string MaintenanceInd { get; set; }
    }
}