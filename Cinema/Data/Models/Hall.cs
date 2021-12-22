using System.Collections.Generic;

namespace Cinema.Models
{
    public class Hall
    {
        public int id { get; set; }
        public string Name { get; set; }

        public List<Seat> Seat { get; set; }

        public List<Session> Session { get; set; } = new List<Session>();
    }
}
