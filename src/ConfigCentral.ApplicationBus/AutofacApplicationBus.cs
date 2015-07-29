using System;
using System.Threading.Tasks;
using Autofac;

namespace ConfigCentral.ApplicationBus
{
    internal class AutofacApplicationBus : IApplicationBus
    {
        private readonly ILifetimeScope _parentLifetimeScope;

        public AutofacApplicationBus(ILifetimeScope parentLifetimeScope)
        {
            _parentLifetimeScope = parentLifetimeScope;
        }

        public async Task<Response<TResponseData>> RequestAsync<TRequest, TResponseData>(
            TRequest request) where TRequest : IRequest<TResponseData>
        {
            var response = new Response<TResponseData>();

            try
            {
                using (var messageScope = _parentLifetimeScope.BeginLifetimeScope("ApplicationMessage"))

                {
                    var handler = CreateHandlerFor<TRequest, TResponseData>(request, messageScope);
                    response.Data = await handler.HandleAsync(request);
                }
            }
            catch (Exception e)
            {
                response.Exception = e;
            }

            return response;
        }

        private IRequestHandler<TRequest, TResponseData> CreateHandlerFor<TRequest, TResponseData>(
            TRequest request,
            ILifetimeScope messageScope) where TRequest : IRequest<TResponseData>
        {
            if (Equals(request, null) || Equals(request, default(TRequest)))
                throw new ArgumentNullException("request");

            Console.WriteLine("AutofacHandlerFactory scope: {0} ({1})",
                messageScope.Tag,
                messageScope.GetType());

            return messageScope.Resolve<IRequestHandler<TRequest, TResponseData>>();
        }
    }
}