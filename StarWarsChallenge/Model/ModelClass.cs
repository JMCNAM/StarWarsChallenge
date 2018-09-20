using System.Collections.Generic;

namespace StartWarsChallenge.Models
{
    // Data model
    public class Starship
    {
        public string name { get; set; }
        public string consumables { get; set; }
        public string MGLT { get; set; }
    }
    // Rsponse model
    public class RequestResults<Starship>
    {
        public string previous { get; set; }
        public string next { get; set; }
        public List<Starship> results { get; set; }
    }
}
