using System;
using System.Threading.Tasks;

namespace ConfigCentral.Mediator
{
    internal class NullHandler<TRequest, TResponseData> : IRequestHandler<TRequest, TResponseData>
        where TRequest : IRequest<TResponseData>
    {
        public async Task<TResponseData> HandleAsync(TRequest request)
        {
            Console.WriteLine("request of type '{0}' handled by '{1}'", request.GetType(), GetType());
            return await Task.FromResult(default(TResponseData));
        }
    }
}