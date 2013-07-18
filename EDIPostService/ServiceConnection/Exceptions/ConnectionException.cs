using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EDIPostService.ServiceConnection.Exceptions
{
    /// <summary>
    /// The connection exception class
    /// </summary>
    [Serializable]
    public class ConnectionException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ConnectionException()
            : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The error message</param>
        public ConnectionException(string message)
            : base(message) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public ConnectionException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ConnectionException(string message, Exception innerException)
            : base(message, innerException) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="format"></param>
        /// <param name="innerException"></param>
        /// <param name="args"></param>
        public ConnectionException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ConnectionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
