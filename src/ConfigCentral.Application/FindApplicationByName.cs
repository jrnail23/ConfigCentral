using System;
using ConfigCentral.ApplicationBus;

namespace ConfigCentral.Application
{
    public class FindApplicationByName : IRequest<ApplicationState>
    {
        private readonly Guid _messageId = Guid.NewGuid();

        public string Name { get; set; }
        public Guid MessageId
        {
            get { return _messageId; }
        }
    }
}