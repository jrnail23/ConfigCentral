using System.Threading.Tasks;

namespace ConfigCentral.ApplicationBus
{
    public interface IRequestHandler<in TRequest, TResponseData> where TRequest : IRequest<TResponseData>
    {
        Task<TResponseData> HandleAsync(TRequest request);
    }
}