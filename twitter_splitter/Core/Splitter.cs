using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    /// <summary>
    /// Splits a single long message into a series of Tweets 
    /// </summary>
    public class Splitter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Splitter"/> class.
        /// </summary>
        /// <param name="splitterConfiguration">The splitter configuration.</param>
        /// <exception cref="NotImplementedException"></exception>
        public Splitter(SplitterConfiguration splitterConfiguration)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Splits the specified message into a series of tweets.
        /// </summary>
        /// <param name="message">The message to be split.</param>
        /// <returns>A series of tweets to be posted to Twitter, in the order in which they should be posted.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<ITweet> Split(string message)
        {
            throw new NotImplementedException();
        }
    }
}
