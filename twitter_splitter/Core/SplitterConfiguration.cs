using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    /// <summary>
    /// Configuration class for a <see cref="Splitter"/>
    /// </summary>
    public class SplitterConfiguration
    {
        /// <summary>
        /// Gets or sets the maximum length of a single tweet.
        /// </summary>
        /// <value>
        /// The maximum length of a tweet.
        /// </value>
        public int MaximumTweetLength { get; set; } = 140;

        /// <summary>
        /// Gets the format of a single tweet.
        /// </summary>
        /// <remarks>
        /// The following placeholders are supported:
        /// <list type="bullet">
        ///   <item>{index} - the index of this single message within the message collection</item>
        ///   <item>{total} - The total number of messages within the message collection</item>
        ///   <item>{mention} - If there was an @mention in the input message, this placeholder will be resolved to the mention.</item>
        ///   <item>{continuation} - The text to indicate this message is a continuation of another message. 
        ///                           Note that this will be an empty string for the first message in the collection. See <see cref="ContinuationText"/></item>
        ///   <item>{continues} - The text to indicate this message will be continued by another message. 
        ///                       Note that this will be an empty string for the last message in the collection. See <see cref="ContinuesText"/></item>
        ///   <item>{message} - The message body</item>
        /// </list>
        /// </remarks>
        /// <value>
        /// The tweet format.
        /// </value>
        public string TweetFormat { get; set; } = "[{index}/{total}] {mention} {continuation}{message}{continues}";

        /// <summary>
        /// Gets or sets the continuation text.
        /// </summary>
        /// <remarks>
        /// This is the text that should replace {continuation} if this tweet follows on from a previous tweet.
        /// If this is the first tweet, {continuation} will be replaced by empty string.
        /// </remarks>
        /// <value>
        /// The continuation text.
        /// </value>
        public string ContinuationText { get; set; } = "< ";

        /// <summary>
        /// Gets or sets the continuation text.
        /// </summary>
        /// <remarks>
        /// This is the text that should replace {continues} if this tweet is followed by another tweet.
        /// If this is the last tweet, {continues} will be replaced by empty string.
        /// </remarks>
        /// <value>
        /// The continuation text.
        /// </value>
        public string ContinuesText { get; set; } = " >";

        /// <summary>
        /// Gets or sets the regular expression used to identify a URL.
        /// </summary>
        /// <value>
        /// The URL regex.
        /// </value>
        public string UrlRegex { get; set; } = "(http|https|ftp)\\:\\/\\/[a-zA-Z0-9\\-\\.]+\\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?\\/?([a-zA-Z0-9\\-\\._\\?\\,\\'/\\\\\\+&%\\$#!\\=~\\)\\(\\*])*[^\\^\\u2026\\<\\\"\\.\\,\\)\\(\\s]";

        /// <summary>
        /// Gets or sets the length of the shortened URL character.
        /// </summary>
        /// <remarks>
        /// Twitter will automatically shorten any URLs it detects. This is the number of characters it will be shortened to.
        /// </remarks>
        /// <value>
        /// The length of the shortened URL character.
        /// </value>
        public int ShortenedUrlCharacterLength { get; set; } = 23;
    }
}
