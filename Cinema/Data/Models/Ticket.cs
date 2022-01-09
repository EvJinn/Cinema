using System.Collections.Generic;

namespace Cinema.Models
{
    /// <summary>
    /// Билет
    /// </summary>
    public class Ticket
    {
        public int id { get; set; }

        public int id_Session { get; set; }
        public Session Session { get; set; }

        public decimal Cost { get; set; }

        public int id_Seat { get; set; }
        public Seat Seat { get; set; }

        public int id_Client { get; set; }
        public Client Client { get; set; }
    }
}
