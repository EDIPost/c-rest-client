using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EDIPostService.Client.Exceptions
{
    [Serializable]
    public class DataException : Exception
    {
        public DataException()
            : base() { }

        public DataException(string message)
            : base(message) { }

        public DataException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public DataException(string message, Exception innerException)
            : base(message, innerException) { }

        public DataException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected DataException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
