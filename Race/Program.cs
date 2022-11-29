using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Race
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"[A-Z]|[a-z]|[0-9]";
            Regex regex = new Regex(pattern);
            List<string> racers = Console.ReadLine().
                Split(", ", StringSplitOptions.RemoveEmptyEntries).
                ToList();
            Dictionary<string, int> results = new Dictionary<string, int>();
            string input;
            while((input = Console.ReadLine()) != "end of race")
            {
                MatchCollection charsOfLine = regex.Matches(input);
                StringBuilder name = new StringBuilder();
                int distance = 0;
                foreach(Match match in charsOfLine)
                {
                    if (Char.IsDigit(match.Value[0]))
                    {
                        distance += int.Parse(match.Value);
                    }
                    else if (Char.IsLetter(match.Value[0]))
                    {
                        name.Append(match.Value);
                    }
                }
                if (racers.Contains(name.ToString()))
                {
                    if (!results.ContainsKey(name.ToString()))
                    {
                        results.Add(name.ToString(), 0);
                    }
                    results[name.ToString()] += distance;
                }

            }
            int places = 1;
            foreach(var kvp in results.OrderByDescending(x => x.Value))
            {
                if( places == 4){
                    break;
                }
                if (places == 1)
                {
                    Console.WriteLine($"1st place: {kvp.Key}");
                }
                else if (places == 2)
                {
                    Console.WriteLine($"2nd place: {kvp.Key}");
                }
                else if (places == 3)
                {
                    Console.WriteLine($"3rd place: {kvp.Key}");
                }
                places++;
            }
        }
    }
}
