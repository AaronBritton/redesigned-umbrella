using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace encryption_helper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hi, Welcome to the Encryptor!");
            Console.WriteLine("Would you like to Encrypt or Decrypt today?");
            string type = Console.ReadLine();
            Console.WriteLine("You entered " + type);
            
            switch (type.ToUpper())
            {
                case "ECRYPT":
                    Encrypt();
                    break;
                case "E":
                    Encrypt();
                    break;
                case "DECRYPT":
                    Decrypt();
                    break;
                case "D":
                    Decrypt();
                    break;
                default:
                    Console.WriteLine("Invalid option supplied :(");
                    Console.WriteLine("Please enter either of the following options next time");
                    Console.WriteLine("Encrypt");
                    Console.WriteLine("E");
                    Console.WriteLine("Decrypt");
                    Console.WriteLine("D");
                    break;
            }
            // End of Encryption/Decryption - pause to allow user to get details
            Console.ReadLine();
        }

        public static void Encrypt()
        {
            Console.WriteLine("Enter phrase to encrypt");
            string phrase = Console.ReadLine();
            Console.WriteLine("Your encrypted string is...");
            Console.WriteLine(EncryptionHelper.Encrypt(phrase));
        }

        public static void Decrypt()
        {
            Console.WriteLine("Enter phrase to decrypt");
            string phrase = Console.ReadLine();
            Console.WriteLine("Your decrypted string is...");
            Console.WriteLine(EncryptionHelper.Decrypt(phrase));
        }
    }

    public static class EncryptionHelper
    {
        private static string GetEncryptionPhrase()
        {
            return "LkUYOR0zxaEZkpmHvRad385mPHw6Llm2";
        }
        public static string Encrypt(string phrase)
        {
            return StringCipher.Encrypt(phrase, GetEncryptionPhrase());
        }

        public static string Decrypt(string phrase)
        {
            return StringCipher.Decrypt(phrase, GetEncryptionPhrase());
        }
    }
}
