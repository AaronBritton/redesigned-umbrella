using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                WriteLine("Please provide exactly 1 argument - the text to split.");
                return;
            }

            var messageInput = args[0];

            var configuration = new SplitterConfiguration();
            var splitter = new Splitter(configuration);
            var tweets = splitter.Split(messageInput);

            for (var i = 0; i < tweets.Count(); i++)
            {
                var tweet = tweets.ElementAt(i);
                WriteLine($"Printing Tweet {i+1} of {tweets.Count()} ({tweet.Message.Length}) characters.");
                WriteLine(tweet.Message);
                WriteLine();
            }
            Console.ReadLine();
        }
    }
}
