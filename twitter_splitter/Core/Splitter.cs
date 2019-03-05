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
        private readonly int _maximumTweetLength;
        private readonly string _tweetformat;
        private readonly string _continuationText;
        private readonly string _continuesText;
        private readonly string _urlRegex;
        private readonly int _shortendUrlCharacterLength;

        private int currentMessageIndex;
        private int currentTweetIndex;
        private int numberOfTweets;
        private Tweet[] Tweets;

        /// <summary>
        /// Initializes a new instance of the <see cref="Splitter"/> class.
        /// </summary>
        /// <param name="splitterConfiguration">The splitter configuration.</param>
        public Splitter(SplitterConfiguration splitterConfiguration)
        {
            _maximumTweetLength = splitterConfiguration.MaximumTweetLength;
            _tweetformat = splitterConfiguration.TweetFormat;
            _continuationText = splitterConfiguration.ContinuationText;
            _continuesText = splitterConfiguration.ContinuesText;
            _urlRegex = splitterConfiguration.UrlRegex;
            _shortendUrlCharacterLength = splitterConfiguration.ShortenedUrlCharacterLength;

            currentTweetIndex = -1;
            currentMessageIndex = 0;
        }

        /// <summary>
        /// Splits the specified message into a series of tweets.
        /// </summary>
        /// <param name="message">The message to be split.</param>
        /// <returns>A series of tweets to be posted to Twitter, in the order in which they should be posted.</returns>
        public IEnumerable<Tweet> Split(string message)
        {
            numberOfTweets = (message.Length + _maximumTweetLength - 1) / _maximumTweetLength;

            Tweets = new Tweet[numberOfTweets];

            if (numberOfTweets == 1)
            {
                AddToTweetList(message);
            }
            else
            {
                // More than one tweet is required, start splitting.
                while(true)
                {
                    if (currentMessageIndex + _maximumTweetLength >= message.Length)
                    {
                        // Only one tweet remains.
                        AddToTweetList(message.Substring(currentMessageIndex));
                        break;
                    }
                    else
                    {
                        // More than one tweet remain - Determine whether the last
                        // character within the current Tweet is valid.
                        char endOfTweetCharacter = message[currentMessageIndex + _maximumTweetLength];
                        if (ValidEndOfTweetCharacter(endOfTweetCharacter))
                        {
                            // The last character is valid - add to list of Tweets
                            AddToTweetList(message.Substring(currentMessageIndex, _maximumTweetLength));
                        }
                        else
                        {
                            // The last character of the current tweet is invalid - loop
                            // backwards until we find the first valid character.
                            for (int j = currentMessageIndex + _maximumTweetLength; j > currentMessageIndex; j--)
                            {
                                endOfTweetCharacter = message[j];
                                if (ValidEndOfTweetCharacter(endOfTweetCharacter))
                                {
                                    AddToTweetList(message.Substring(currentMessageIndex, j-currentMessageIndex));
                                    break;
                                }
                            }
                        }
                    }

                }
            }

            return Tweets;
        }

        /// <summary>
        /// Determine whether an character is valid for the end of a tweet
        /// </summary>
        /// <param name="lastCharacter">The character which should be checked</param>
        /// <returns>True if <see cref="lastCharacter"/> is valid for the end of a tweet</returns>
        private bool ValidEndOfTweetCharacter(char lastCharacter)
        {
            return char.IsPunctuation(lastCharacter) || char.IsWhiteSpace(lastCharacter);
        }

        /// <summary>
        /// Add an tweet to the list of tweets
        /// </summary>
        /// <param name="message">The message to be tweeted</param>
        private void AddToTweetList(string message)
        {
            currentTweetIndex++;

            // Resize the Tweets array if required
            if (currentTweetIndex >= numberOfTweets-1)
            {
                Array.Resize(ref Tweets, numberOfTweets++);
            }

            Tweets[currentTweetIndex] = new Tweet(message);

            // Update the current position within the message
            currentMessageIndex += message.Length;
        }
    }
}
