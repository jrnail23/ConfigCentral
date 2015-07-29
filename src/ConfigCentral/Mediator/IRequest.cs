using System;

namespace ConfigCentral.Mediator
{
    public interface IRequest<TResponseData>
    {
        Guid MessageId { get; }
    }
}