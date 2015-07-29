using System;
using System.Collections.Generic;
using System.Linq;
using ConfigCentral.DomainModel.Impl;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace ConfigCentral.Infrastructure.DataAccess
{
    public class NHibernateConfiguration
    {
        private static readonly IEnumerable<Type> MappingTypes = typeof (ApplicationMapping).Assembly.GetTypes()
            .Where(t => typeof (IConformistHoldersProvider).IsAssignableFrom(t) && !t.IsAbstract)
            .ToList();
        private readonly string _connectionString;

        public NHibernateConfiguration(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Configuration Configure()
        {
            var config = new Configuration();
            config.DataBaseIntegration(db =>
            {
                db.ConnectionString = _connectionString;
                db.Driver<SqlServerCeDriver>();
                db.Dialect<MsSqlCe40Dialect>();
                //db.IsolationLevel = IsolationLevel.ReadCommitted;
            });
            
            var mapping = CompileMapping();
            config.AddDeserializedMapping(mapping, null);

            return config;
        }

        private static HbmMapping CompileMapping()
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(MappingTypes);
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }
    }
}