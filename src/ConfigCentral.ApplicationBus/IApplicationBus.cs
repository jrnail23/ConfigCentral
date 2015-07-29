using System.Threading.Tasks;

namespace ConfigCentral.ApplicationBus
{
    public interface IApplicationBus
    {
        Task<Response<TResponseData>> RequestAsync<TRequest, TResponseData>(TRequest request)
            where TRequest : IRequest<TResponseData>;
    }
}