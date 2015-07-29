using System.Threading.Tasks;

namespace ConfigCentral.Mediator
{
    public interface IRequestHandler<in TRequest, TResponseData> where TRequest : IRequest<TResponseData>
    {
        Task<TResponseData> HandleAsync(TRequest request);
    }
}