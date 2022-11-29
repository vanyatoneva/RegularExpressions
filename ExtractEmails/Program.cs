using System;
using System.Text.RegularExpressions;

namespace ExtractEmails
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"[\ \n](?<email>[\da-zA-Z]+[\._\-\da-zA-Z]*@[a-zA-Z]+([\-\.a-zA-Z])*\.[\-a-zA-Z]+)";
            string input = Console.ReadLine();
            foreach(Match match in Regex.Matches(input, pattern))
            {
                Console.WriteLine(match.Groups["email"].Value);
            }
        }
    }
}
