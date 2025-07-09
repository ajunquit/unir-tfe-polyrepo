using System.Runtime.Serialization;

namespace Addition.Service.Domain.Exceptions
{
    public class AdditionOperationNotImplemented : Exception
    {
        public string Code { get; set; }

        public AdditionOperationNotImplemented() : base() { }

        public AdditionOperationNotImplemented(string message) : base(message, null) { }

        public AdditionOperationNotImplemented(string code, string message) : base(message, null)
        {
            Code = code;
        }

        public AdditionOperationNotImplemented(string message, Exception innerException) : base(message, innerException) { }

        protected AdditionOperationNotImplemented(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
