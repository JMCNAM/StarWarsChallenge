using System;
using System.Collections.Generic;
using StartWarsChallenge.Models;
using StartWarsChallenge.Requests;

namespace StartWarsChallenge.Process
{
    public class ProcessClass
    {
        public Dictionary<Starship, string> ComputeStarshipResupply(int distance)
        {
            Dictionary<Starship, string> result = new Dictionary<Starship, string>();
            List<Starship> allStarShips = null;
            try
            {
                allStarShips = GetAllStarships().results;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (allStarShips != null)
            {
                foreach (var ship in allStarShips)
                {
                    var numResupplies = "";
                    if ((ship.consumables != "unknown"))
                    {
                        if (ship.MGLT != "unknown")
                        {
                            var speed = Convert.ToInt32(ship.MGLT);
                            var hoursBetweenResupply = ConvertToHours(ship.consumables);
                            numResupplies = Convert.ToString(distance / (speed * hoursBetweenResupply));
                            result.Add(ship, numResupplies);
                        }
                        else
                            result.Add(ship, "unknown");
                    }
                    else
                        result.Add(ship, "unknown");
                }
                return result;
            }
            else
                return null;
        }

        private RequestResults<Starship> GetAllStarships(string entityName = "/starships/")
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("page", "1");
            RequestClass req = new RequestClass();
            RequestResults<Starship> result = req.GetByUrl<Starship>(entityName, parameters);

            if (result != null)
            {
                while (result.next != null)
                {
                    parameters["page"] = (Convert.ToInt32(parameters["page"]) + 1).ToString();
                    RequestResults<Starship> nextResult = req.GetByUrl<Starship>(entityName, parameters);

                    result.results.InsertRange(result.results.Count, nextResult.results);
                    result.next = String.IsNullOrEmpty(result.next) ? null : nextResult.next;
                }
            }
            return result;
        }

        private int ConvertToHours(string input)
        {
            var timeAmount = Convert.ToInt32(input.Split(' ')[0]);
            var timeUnits = input.Split(' ')[1];

            if (timeUnits[timeUnits.Length - 1] == 's')
            {
                timeUnits = timeUnits.Remove(timeUnits.Length - 1);
            }
            var conversion = MapHours[timeUnits];
            return Convert.ToInt32(conversion * timeAmount);
        }

        private Dictionary<string, int> MapHours
        = new Dictionary<string, int> {
            { "hour", 1 },
            { "day", 24 },
            { "week", 168 },
            { "month", 730},
            { "year", 8760}
        };
    }
}
