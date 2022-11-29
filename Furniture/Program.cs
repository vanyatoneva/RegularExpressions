using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Furniture
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            string pattern = @">>(?<furnitureName>[A-Za-z]+)<<(?<price>[0-9]+(\.[0-9]+){0,1})!(?<quantity>[0-9]+)";
            Regex regex = new Regex(pattern);
            decimal totalMoneySpent = 0.00m;
            List<string> furnitureBought = new List<string>();
            string currentLine;
            while((currentLine = Console.ReadLine()) != "Purchase")
            {
                Match match = regex.Match(currentLine);
                if (match.Success)
                {

                    furnitureBought.Add(match.Groups["furnitureName"].Value);
                    
                    decimal quantity = decimal.Parse(match.Groups["quantity"].Value);
                    decimal price = decimal.Parse(match.Groups["price"].Value);
                    totalMoneySpent +=  (price * quantity);
                }
            }
            Console.WriteLine("Bought furniture:");
            
            foreach(string furniture  in furnitureBought)
            {
                Console.WriteLine(furniture);
                
            }
            Console.WriteLine($"Total money spend: {totalMoneySpent:f2}");
        }
    }
}
