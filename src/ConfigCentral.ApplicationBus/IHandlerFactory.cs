namespace ConfigCentral.ApplicationBus
{
    interface IHandlerFactory
    {
        IRequestHandler<TRequest, TResponseData> CreateHandlerFor<TRequest, TResponseData>(TRequest request)
            where TRequest : IRequest<TResponseData>;
    }
}