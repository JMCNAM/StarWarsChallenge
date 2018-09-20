using StartWarsChallenge.Process;
using System;


namespace Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ProcessClass p = new ProcessClass();
            Console.WriteLine("| - StarWars Code Challenge - |\n" +
                "\nEnter a integer distance in Mega-Lightyears: ");
            var distance = Convert.ToInt32(Console.ReadLine());
            var res = p.ComputeStarshipResupply(distance);
            if (res != null)
            {
                Console.WriteLine("\n| {0, -30} | {1, 14} |", "Ship Name", "Num Resupplies");
                foreach (var ship in res)
                {
                    Console.WriteLine("| {0, -30} | {1, 14} |", ship.Key.name, ship.Value);
                }
            }
            else
            {
                Console.WriteLine("ERROR : Result was null.");
            }
            Console.ReadKey();
        }
    }
}
