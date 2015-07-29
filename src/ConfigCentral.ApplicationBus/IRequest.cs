using System;

namespace ConfigCentral.ApplicationBus
{
    public interface IRequest<TResponseData>
    {
        Guid MessageId { get; }
    }
}