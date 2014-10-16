using System.Collections.Generic;
using System.Linq;

namespace ConfigCentral.Domain
{
    /// <summary>
    ///     Adapted from https://github.com/VaughnVernon/IDDD_Samples_NET
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        ///     When overriden in a derived class, returns all components of a value objects which constitute its identity.
        /// </summary>
        /// <returns>An ordered list of equality components.</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }
            var vo = obj as ValueObject;
            if (vo == null)
            {
                return false;
            }
            return GetEqualityComponents()
                .SequenceEqual(vo.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return HashCodeHelper.CombineHashCodes(GetEqualityComponents());
        }
    }
}