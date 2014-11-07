using System;
using System.Runtime.Serialization;

namespace ConfigCentral.DomainModel
{
    [Serializable]
    public class DuplicateObjectException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public DuplicateObjectException() {}
        public DuplicateObjectException(string message) : base(message) {}
        public DuplicateObjectException(string message, Exception inner) : base(message, inner) {}

        protected DuplicateObjectException(SerializationInfo info,
            StreamingContext context) : base(info, context) {}
    }
}