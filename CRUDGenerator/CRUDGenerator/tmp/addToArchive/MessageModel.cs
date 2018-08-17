using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess.DataAccessObject
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="MessageModel"/> is status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if status; otherwise, <c>false</c>.
        /// </value>
        public bool status { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string message { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public dynamic data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageModel"/> class.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        /// <param name="message">The message.</param>
        /// <param name="data">The data.</param>
        public MessageModel(bool status, string message, object data)
        {
            this.status = status;
            this.message = message;
            this.data = data;
        }
    }
}