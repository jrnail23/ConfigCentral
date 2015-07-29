namespace ConfigCentral.Mediator
{
    interface IHandlerFactory
    {
        IRequestHandler<TRequest, TResponseData> CreateHandlerFor<TRequest, TResponseData>(TRequest request)
            where TRequest : IRequest<TResponseData>;
    }
}