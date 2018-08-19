namespace ClosDotNet.Domain.Base.ORM
{
    using NHibernate.Mapping.ByCode;
    using Spring.Data.NHibernate;
    using System.Linq;
    using System.Reflection;

    public class DualMappingLocalSessionFactoryObject : LocalSessionFactoryObject
    {
        public DualMappingLocalSessionFactoryObject()
        {
            ClassMappingAssemblies = new string[] { };
        }

        public string[] ClassMappingAssemblies { get; set; }

        protected override void PostProcessConfiguration(NHibernate.Cfg.Configuration config)
        {
            base.PostProcessConfiguration(config);

            var mapper = new ModelMapper();

            foreach (var asm in ClassMappingAssemblies.Select(Assembly.Load))
            {
                mapper.AddMappings(asm.GetTypes());
            }

            foreach (var mapping in mapper.CompileMappingForEachExplicitlyAddedEntity())
            {
                config.AddMapping(mapping);
            }

        }
    }
}