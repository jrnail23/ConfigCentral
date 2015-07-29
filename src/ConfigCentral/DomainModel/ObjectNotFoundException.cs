using System;
using System.Runtime.Serialization;

namespace ConfigCentral.DomainModel
{
    [Serializable]
    public class ObjectNotFoundException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ObjectNotFoundException() {}
        public ObjectNotFoundException(string message) : base(message) {}
        public ObjectNotFoundException(string message, Exception inner) : base(message, inner) {}

        protected ObjectNotFoundException(SerializationInfo info,
            StreamingContext context) : base(info, context) {}
    }


}