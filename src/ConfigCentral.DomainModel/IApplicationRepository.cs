using System.Collections.Generic;

namespace ConfigCentral.DomainModel
{
    public interface IApplicationRepository {
        Application FindByName(string name);
        IEnumerable<Application> All();
        void Add(Application application);
    }
}