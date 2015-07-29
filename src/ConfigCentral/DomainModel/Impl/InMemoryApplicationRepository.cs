using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigCentral.DomainModel.Impl
{
    public class InMemoryApplicationRepository : IApplicationRepository
    {
        public static readonly ISet<Application> AppsDataStore = new HashSet<Application>();

        public Application FindByName(string name)
        {
            var app = AppsDataStore.SingleOrDefault(a => a.Name == name);
            if (app == null)
            {
                throw new ObjectNotFoundException($"Could not find an application named '{name}'.");
            }
            return app;
        }

        public IEnumerable<Application> All()
        {
            return AppsDataStore;
        }

        public void Add(Application application)
        {
            if (AppsDataStore.Add(application))
            {
                return;
            }

            throw new Exception($"An application named '{application.Name}' already exists.");
        }
    }
}