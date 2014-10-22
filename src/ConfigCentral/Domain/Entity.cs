using System;
using System.Collections.Generic;
using System.Linq;
using ConfigCentral.Domain.Model;

namespace ConfigCentral.Domain
{
    // TODO: consider entity versioning
    // TODO: consider audit stamps

    public abstract class Entity<T> where T:Identifier
    {
        [Obsolete("Do not use - only here to support tooling", error: true)]
        protected Entity() {}

        private readonly T _id;

        protected Entity(T id)
        {
            _id = id;
        }

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
            var vo = obj as Entity<T>;
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

        public T Id
        {
            get { return _id; }
        }

        /// <summary>
        ///     When overriden in a derived class, returns all components of a value objects which constitute its identity.
        /// </summary>
        /// <returns>An ordered list of equality components.</returns>
        protected virtual IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}