using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NetherRealms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string daemonNamePattern = @"(?<daemonName>[^\ \,]+)";
            List<string> daemonsNames = Console.ReadLine().
                Split(",", StringSplitOptions.RemoveEmptyEntries).
                ToList();
            List<Daemon> daemons = new List<Daemon>();
            foreach (string daemonName in daemonsNames)
            {
                Match daemonMatch = Regex.Match(daemonName, daemonNamePattern);
                if (daemonMatch.Success)
                {
                    daemons.Add(new Daemon(daemonMatch.ToString()));
                }
            }
            foreach (Daemon daemon in daemons.OrderBy(x => x.name))
            {
                Console.WriteLine($"{daemon.name} - {daemon.health} health, {daemon.damage:f2} damage");
            }
        }
    }

    public class Daemon
    {
        public Daemon(string name)
        {
            this.name = name;
            this.damage = GetDaemonDamage(name);
            this.health = GetDaemonHealth(name);
        }
        public string name { get; set; }
        public double health {  get; private set; }
        public decimal damage { get; private set; }

        private string getCharsForHealthPattern = @"[^\d\/\*\+\-\.]";
        private string getNumbersForDamagePattern = @"(?<sum>(-|\+){0,1}\d+((\.\d+){0,1}))|(?<multiplyOrDivideByTwo>\/|\*)";

        private double GetDaemonHealth(string name)
        {
            double health = 0.00;
            foreach(Match match in Regex.Matches(name, getCharsForHealthPattern))
            {
                health += match.Value.ToString()[0];
            }
            return health;
        }

        private decimal GetDaemonDamage(string name)
        {
            decimal damage = 0.00m;
            //Multiply and division must be after sum of all numbers, so keep the num of Operations
            int multiplyByTwoCount = 0;
            int divideByTwoCount = 0;
            //Get all the numbers and multiply and divisions by Two
            MatchCollection matches = Regex.Matches(name, getNumbersForDamagePattern);
            foreach (Match m in matches)
            {
                string operation = m.Value;
                if(operation == "*")
                {
                    multiplyByTwoCount++;
                    continue;
                }
                if(operation == "/")
                {
                    divideByTwoCount++;
                    continue;
                }
                char plusOrMinus = operation[0] == '-' ? '-' : '+';
                if (operation[0] == '+' || operation[0] == '-')
                {
                    operation = operation.Substring(1);
                }
                decimal number = decimal.Parse(operation);
                if(plusOrMinus == '-')
                {
                    damage -= number;
                }
                else if(plusOrMinus == '+')
                {
                    damage += number;
                }
            }
            //Get just the number of multiplies and divides that need
            if(multiplyByTwoCount > divideByTwoCount)
            {
                multiplyByTwoCount = multiplyByTwoCount - divideByTwoCount;
                divideByTwoCount = 0;
                for(int i = 0; i < multiplyByTwoCount; i++)
                {
                    damage *= 2;
                }
            }
            else if(multiplyByTwoCount == divideByTwoCount)
            {
                multiplyByTwoCount = 0;
                divideByTwoCount = 0;
            }
            else if(multiplyByTwoCount < divideByTwoCount)
            {
                divideByTwoCount = divideByTwoCount - multiplyByTwoCount;
                multiplyByTwoCount = 0;
                for(int i = 0; i < divideByTwoCount; i++)
                {
                    damage /= 2;
                }
            }
            return damage;
        }

    }
}
