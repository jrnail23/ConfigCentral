using System.Threading.Tasks;
using ConfigCentral.DomainModel;
using ConfigCentral.Mediator;

namespace ConfigCentral.UseCases
{
    class FindApplicationByNameHandler : IRequestHandler<FindApplicationByName, ApplicationState> {
        private readonly IApplicationRepository _repository;

        public FindApplicationByNameHandler(IApplicationRepository repository)
        {
            _repository = repository;
        }

        public Task<ApplicationState> HandleAsync(FindApplicationByName request)
        {
            var application = _repository.FindByName(request.Name);
            return Task.FromResult(new ApplicationState
            {
                Name = application.Name
            });
        }
    }
}