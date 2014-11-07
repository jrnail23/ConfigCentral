using System;

namespace ConfigCentral.DomainModel
{
    public class Application : IEquatable<Application>
    {
        private readonly Guid _id;
        private readonly string _name;
        private int _version = 0;

        [Obsolete("Do not use - only here to support tooling", error: true)]
        protected Application() {}
        public Application(Guid id, string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (name == string.Empty) throw new ArgumentException("value must not be empty", "name");

            _id = id;
            _name = name;
        }

        public virtual string Name
        {
            get { return _name; }
        }
        public virtual int Version
        {
            get { return _version; }
        }
        public virtual Guid Id
        {
            get { return _id; }
        }

        public virtual bool Equals(Application other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(_name, other._name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Application) obj);
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

        public static bool operator ==(Application left, Application right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Application left, Application right)
        {
            return !Equals(left, right);
        }
    }
}