﻿using System;

namespace ConfigCentral.DomainModel
{
    public class Application : IEquatable<Application>
    {
        private readonly string _name;

        public Application(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (name == string.Empty) throw new ArgumentException("value must not be empty", "name");

            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public bool Equals(Application other)
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