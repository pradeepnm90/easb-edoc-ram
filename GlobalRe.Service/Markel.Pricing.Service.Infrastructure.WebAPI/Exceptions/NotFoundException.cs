using System;
using System.Runtime.Serialization;

namespace Markel.Pricing.Service.Infrastructure.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        private const string _exceptionMessage = "Not Found"; //TODO: need to move hardcoded values to resource files
        public NotFoundException()
            : base(_exceptionMessage) { }

        public NotFoundException(string message)
            : base(message)
        {
        }

        public NotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException) { }

        public NotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}