using System.Threading.Tasks;

namespace ConfigCentral.Mediator
{
    public interface IApplicationBus
    {
        Task<Response<TResponseData>> RequestAsync<TRequest, TResponseData>(TRequest request)
            where TRequest : IRequest<TResponseData>;
    }
}