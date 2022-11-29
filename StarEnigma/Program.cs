using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace StarEnigma
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string patternForDecrypt = @"s|S|t|T|a|A|r|R";
            Regex deccryptKey = new Regex(patternForDecrypt);

            string pattern = @"[^\@\-\!\:\>]*@(?<planet>[A-Za-z]+)[^\@\-\!\:\>]*:(?<population>\d+)[^\@\-\!\:\>]*!(?<attackType>A|D)![^\@\-\!\:\>]*->(?<soldiers>\d+)[^\@\-\!\:\>]*";
            Regex regex = new Regex(pattern);

            int n = int.Parse(Console.ReadLine());
            List<string> attackedPlanets = new List<string>();
            List<string> destroyedPlanets = new List<string>();
            for(int i = 0;  i < n; i++)
            {
                string message = DecryptMessage(deccryptKey);
                Match match = regex.Match(message);
                if (match.Success)
                {
                    string planet = match.Groups["planet"].Value;
                    string attachType = match.Groups["attackType"].Value;
                    if(attachType == "A")
                    {
                        attackedPlanets.Add(planet);
                    }
                    else if(attachType == "D")
                    {
                        destroyedPlanets.Add(planet);
                    }
                }
            
            }
            Console.WriteLine($"Attacked planets: {attackedPlanets.Count}");
            foreach(string planet in attackedPlanets.OrderBy(x=> x)){
                Console.WriteLine($"-> {planet}");
            }
            Console.WriteLine($"Destroyed planets: {destroyedPlanets.Count}");
            foreach (string planet in destroyedPlanets.OrderBy(x => x))
            {
                Console.WriteLine($"-> {planet}"); ;
            }
        }

        private static string DecryptMessage(Regex deccryptKey)
        {
            string message = Console.ReadLine();
            MatchCollection matches = deccryptKey.Matches(message);
            int decryptionKey = matches.Count;
            StringBuilder sb = new StringBuilder();
            foreach (char c in message)
            {
                sb.Append((char)(c - decryptionKey));
            }
            return sb.ToString();
        }
    }
}
