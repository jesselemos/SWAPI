using SWAPI.Business;
using SWAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SWAPI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            WriteHeader();
            var input = Console.ReadLine();
            while (input != "q")
            {
                Console.Clear();
                ulong distance = ulong.TryParse(input, out distance) ? distance : 1000000;

                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"Calculating Starships stops to: {distance} MGLT");
                Console.WriteLine("------------------------------------------------");
                List<StarshipModel> calculatedStops = StarshipBusiness.GetStarshipStops(distance);
                Console.Clear();

                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"Calculated Starships stops to: {distance} MGLT");
                Console.WriteLine("------------------------------------------------");
                calculatedStops.Where(x => x.stops > 0).ToList().ForEach(x => Console.WriteLine($"{x.name}: {x.stops}"));

                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("\nThe Starships below do not have enough information to calculate:");
                Console.WriteLine("-----------------------------------------------------------------");
                calculatedStops.Where(x => x.stops == 0).ToList().ForEach(x => Console.WriteLine($"{x.name}: {x.stops}"));

                WriteHeader();
                input = Console.ReadLine();
            }

            Console.Clear();
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Thank you, I hope you enjoyed!");
            Console.WriteLine("------------------------------------------");
        }

        private static void WriteHeader()
        {
            Console.WriteLine("\n-----------------------------------------------------------------");
            Console.WriteLine("SWAPI API Test! Input q to quit!");
            Console.WriteLine("Input a distance in MGLT (mega lights, default value is 1000000):");
            Console.WriteLine("-----------------------------------------------------------------");
        }
    }
}
