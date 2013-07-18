using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EDIPostService.Client.Exceptions
{
    /// <summary>
    /// Dataexception class
    /// </summary>
    [Serializable]
    public class DataException : Exception
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public DataException()
            : base() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public DataException(string message)
            : base(message) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public DataException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public DataException(string message, Exception innerException)
            : base(message, innerException) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="format"></param>
        /// <param name="innerException"></param>
        /// <param name="args"></param>
        public DataException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected DataException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
