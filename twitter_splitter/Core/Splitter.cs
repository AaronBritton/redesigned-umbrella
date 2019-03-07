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

        private int _currentMessageIndex;
        private int _numberOfTweets;
        private int _numberOfMentions;
        private Tweet[] Tweets;
        private string[] tweetMessages;
        private string[] tweetMentions;
        private string mentions;

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

            _currentMessageIndex = 0;
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
            message = ExtractMentions(message);

            _maximumTweetLength -= mentions.Length;

            while(true)
            {
                if (_currentMessageIndex + _maximumTweetLength >= message.Length)
                {
                    // Only one tweet remains.
                    AddToTweetMessages(message.Substring(_currentMessageIndex));
                    break;
                }
                else
                {
                    // More than one tweet remain - Determine whether the last
                    // character within the current Tweet is valid.
                    char endOfTweetCharacter = message[_currentMessageIndex + _maximumTweetLength];
                    if (ValidEndOfTweetCharacter(endOfTweetCharacter))
                    {
                        // The last character is valid - add to list of Tweets
                        AddToTweetMessages(message.Substring(_currentMessageIndex, _maximumTweetLength));
                    }
                    else
                    {
                        // The last character of the current tweet is invalid - loop
                        // backwards until we find the first valid character.
                        for (int j = _currentMessageIndex + _maximumTweetLength; j > _currentMessageIndex; j--)
                        {
                            endOfTweetCharacter = message[j];
                            if (ValidEndOfTweetCharacter(endOfTweetCharacter))
                            {
                                AddToTweetMessages(message.Substring(_currentMessageIndex, j-_currentMessageIndex));
                                break;
                            }
                        }
                    }
                }
            }

            if (_numberOfTweets >= 0)
            {

                Tweets = new Tweet[_numberOfTweets];
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

        /// <summary>
        /// Extract out any mentions which are present within the tweet
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string ExtractMentions(string message)
        {
            string strippedOutMessage = message;
            if (message.Contains("@"))
            {
                // There is atleast one mention present
                var splitMessage = message.Split(' ');
                bool foundAllStartingMentions = false;

                foreach(string singleMessage in splitMessage)
                {

                    if (singleMessage[0] == '@')
                    {
                        AddToTweetMentions(singleMessage);
                        if (!foundAllStartingMentions)
                        {
                            strippedOutMessage = strippedOutMessage.Substring(singleMessage.Length);                            
                        }
                    }
                    else
                    {
                        foundAllStartingMentions = true;
                    }
                }
            }

            mentions = string.Join(' ', tweetMentions);

            return strippedOutMessage;
        }

        /// <summary>
        /// Add a mention to the list of required mentions
        /// </summary>
        /// <param name="mention">The mention to be added</param>
        private void AddToTweetMentions(string mention)
        {
            _numberOfMentions++;
            Array.Resize(ref tweetMentions, _numberOfMentions);
            tweetMentions[_numberOfMentions - 1] = mention;
        }

        /// <summary>
        /// Add a message to the list of required tweets
        /// </summary>
        /// <param name="message">The text of an single tweet</param>
        private void AddToTweetMessages(string message)
        {
            _numberOfTweets++;
            Array.Resize(ref tweetMessages, _numberOfTweets);
            tweetMessages[_numberOfTweets-1] = message.Trim();

            _currentMessageIndex += message.Length;
        }

        /// <summary>
        /// Add an tweet to the list of tweets
        /// </summary>
        /// <param name="message">The message to be tweeted</param>
        private void CreateTweets()
        {
            int currentTweetIndex = -1;
            foreach (string message in tweetMessages)
            {
                if (currentTweetIndex == 0)
                {
                    _maximumTweetLength -= _continuationText.Length;
                }

                currentTweetIndex++;

                //// Resize the Tweets array if required
                //if (currentTweetIndex >= _numberOfTweets - 1)
                //{
                //    Array.Resize(ref Tweets, _numberOfTweets++);
                //}

                var tweetMessage = Formatter(currentTweetIndex + 1, _numberOfTweets, mentions, message, currentTweetIndex == 0 ? "" : _continuationText, currentTweetIndex == _numberOfTweets-1 ? "" : _continuesText);

                Tweets[currentTweetIndex] = new Tweet(tweetMessage);
            }
        }

        private string Formatter(int index, int total, string mention, string message, string continuation, string continues) => $"[{index}/{total}] {mention} {continuation}{message}{continues}";

        #endregion
    }
}
