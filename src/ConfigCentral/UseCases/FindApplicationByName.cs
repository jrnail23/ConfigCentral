using System;
using ConfigCentral.Mediator;

namespace ConfigCentral.UseCases
{
    public class FindApplicationByName : IRequest<ApplicationState>
    {
        public string Name { get; set; }
        public Guid MessageId { get; } = Guid.NewGuid();
    }
}