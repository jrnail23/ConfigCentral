using System.Collections.Generic;

namespace ConfigCentral.Domain.Model
{
    public abstract class Identifier : ValueObject
    {
        protected abstract object Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}