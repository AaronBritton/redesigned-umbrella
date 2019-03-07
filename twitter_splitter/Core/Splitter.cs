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
        #region Private Members

        private int _maximumTweetLength;
        private readonly string _tweetformat;
        private readonly string _continuationText;
        private readonly string _continuesText;
        private readonly string _urlRegex;
        private readonly int _shortendUrlCharacterLength;

        private int currentMessageIndex;
        private int currentTweetIndex;
        private int numberOfTweets;
        private Tweet[] Tweets;
        private string[] tweetMessages;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Splitter"/> class.
        /// </summary>
        /// <param name="splitterConfiguration">The splitter configuration.</param>
        public Splitter(SplitterConfiguration splitterConfiguration)
        {
            _maximumTweetLength = splitterConfiguration.MaximumTweetLength;
            _maximumTweetLength -= 7;
            //_maximumTweetLength -= splitterConfiguration.ContinuationText.Length;
            _maximumTweetLength -= splitterConfiguration.ContinuesText.Length;


            _tweetformat = splitterConfiguration.TweetFormat;
            _continuationText = splitterConfiguration.ContinuationText;
            _continuesText = splitterConfiguration.ContinuesText;
            _urlRegex = splitterConfiguration.UrlRegex;
            _shortendUrlCharacterLength = splitterConfiguration.ShortenedUrlCharacterLength;

            currentTweetIndex = -1;
            currentMessageIndex = 0;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Splits the specified message into a series of tweets.
        /// </summary>
        /// <param name="message">The message to be split.</param>
        /// <returns>A series of tweets to be posted to Twitter, in the order in which they should be posted.</returns>
        public IEnumerable<Tweet> Split(string message)
        {   
            while(true)
            {
                if (currentMessageIndex + _maximumTweetLength >= message.Length)
                {
                    // Only one tweet remains.
                    AddToTweetMessages(message.Substring(currentMessageIndex));
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
                        AddToTweetMessages(message.Substring(currentMessageIndex, _maximumTweetLength));
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
                                AddToTweetMessages(message.Substring(currentMessageIndex, j-currentMessageIndex));
                                break;
                            }
                        }
                    }
                }
            }

            if (numberOfTweets >= 0)
            {

                Tweets = new Tweet[numberOfTweets];
                CreateTweets();
                return Tweets;
            }
            else
            {
                return null;
            }
            
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Determine whether an character is valid for the end of a tweet
        /// </summary>
        /// <param name="lastCharacter">The character which should be checked</param>
        /// <returns>True if <see cref="lastCharacter"/> is valid for the end of a tweet</returns>
        private bool ValidEndOfTweetCharacter(char lastCharacter)
        {
            return char.IsPunctuation(lastCharacter) || char.IsWhiteSpace(lastCharacter);
        }

        private void AddToTweetMessages(string message)
        {
            numberOfTweets++;
            Array.Resize(ref tweetMessages, numberOfTweets);
            tweetMessages[numberOfTweets-1] = message;

            currentMessageIndex += message.Length;
        }

        /// <summary>
        /// Add an tweet to the list of tweets
        /// </summary>
        /// <param name="message">The message to be tweeted</param>
        private void CreateTweets()
        {
            foreach (string message in tweetMessages)
            {
                if (currentTweetIndex == 0)
                {
                    _maximumTweetLength -= _continuationText.Length;
                }

                currentTweetIndex++;

                //// Resize the Tweets array if required
                //if (currentTweetIndex >= numberOfTweets - 1)
                //{
                //    Array.Resize(ref Tweets, numberOfTweets++);
                //}

                var tweetMessage = Formatter(currentTweetIndex + 1, numberOfTweets, "", message, currentTweetIndex == 0 ? "" : _continuationText, currentTweetIndex == numberOfTweets-1 ? "" : _continuesText);

                Tweets[currentTweetIndex] = new Tweet(tweetMessage);
            }
        }

        private string Formatter(int index, int total, string mention, string message, string continuation, string continues) => $"[{index}/{total}] {mention} {continuation}{message}{continues}";

        #endregion
    }
}
