using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    /// <summary>
    /// Represents a Tweet that can be posted to Twitter.
    /// </summary>
    public interface ITweet
    {
        /// <summary>
        /// Gets or sets the message that will be posted to Twitter.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; set; }
    }
}
