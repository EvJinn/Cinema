using System.Collections.Generic;

namespace Cinema.Models
{
    public class Seat
    {
        public int id { get; set; }

        public int Number { get; set; }
        public int Row { get; set; }

        public int id_Category { get; set; }
        public SeatCategory SeatCategory { get; set; }

        public int id_Hall { get; set; }
        public Hall Hall { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
