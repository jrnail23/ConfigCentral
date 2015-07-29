using System;

namespace ConfigCentral.ApplicationBus
{
    public class Response<TResponseData>
    {
        private TResponseData _data;
        public virtual TResponseData Data
        {
            get
            {
                if (HasException())
                {
                    throw Exception;
                }
                return _data;
            }
            set { _data = value; }
        }

        public virtual Exception Exception { get; set; }

        public virtual bool HasException()
        {
            return Exception != null;
        }
    }

    public sealed class Response : Response<VoidType>
    {
        public override VoidType Data
        {
            get { return VoidType.Default; }
            set { }
        }
    }
}