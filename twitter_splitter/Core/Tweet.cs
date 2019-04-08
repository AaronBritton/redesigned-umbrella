using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Core
{
    public class Tweet : ITweet
    {
        public string Message { get; set; }

        public Tweet (string message)
        {
            Message = message.Trim();
        }
    }

    public class Tweets : IEnumerable
    {
        private Tweet[] _tweets;
        public Tweets(Tweet[] tweetArray)
        {
            _tweets = new Tweet[tweetArray.Length];

            for (int i = 0; i < tweetArray.Length; i++)
            {
                _tweets[i] = tweetArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public TweetsEnum GetEnumerator()
        {
            return new TweetsEnum(_tweets);
        }
    }

    public class TweetsEnum : IEnumerator
    {
        public Tweet[] _tweets;

        int position = -1;

        public TweetsEnum(Tweet[] list)
        {
            _tweets = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _tweets.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Tweet Current
        {
            get
            {
                try
                {
                    return _tweets[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
