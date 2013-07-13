using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EDIPostService.ServiceConnection.Exceptions
{
    [Serializable]
    public class ConnectionException : Exception
    {
        public ConnectionException()
            : base() { }

        public ConnectionException(string message)
            : base(message) { }

        public ConnectionException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public ConnectionException(string message, Exception innerException)
            : base(message, innerException) { }

        public ConnectionException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected ConnectionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
