using System;
using System.Text.RegularExpressions;

namespace SoftuniBarIncome
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"[^\%\.\|\$]*?%(?<customer>[A-z][a-z]+)%[^\%\.\|\$]*?<(?<product>\w+)\>[^\%\.\|\$]*?\|(?<quantity>\d+)\|([^\%\.\|\$])*?(?<price>\d+(.\d+)?)\$[^\%\.\|\$]*";
            decimal totalIncome = 0.00m;
            Regex regex = new Regex(pattern);
            string input;
            while((input = Console.ReadLine()) != "end of shift")
            {
                Match match = regex.Match(input);
                if (match.Success)
                {
                    decimal sum = decimal.Parse(match.Groups["quantity"].Value) 
                        * decimal.Parse(match.Groups["price"].Value);
                    Console.WriteLine($"{match.Groups["customer"]}: {match.Groups["product"]} - {sum:f2}");
                    totalIncome += sum;
                }
            }
            Console.WriteLine($"Total income: {totalIncome:f2}");
        }
    }
}
