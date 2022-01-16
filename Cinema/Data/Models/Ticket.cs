using System.Collections.Generic;

namespace Cinema.Models
{
    /// <summary>
    /// Билет
    /// </summary>
    public class Ticket
    {
        public int id { get; set; }

        public int id_session { get; set; }
        public Session Session { get; set; }

        public decimal Cost { get; set; }

        public int id_seat { get; set; }
        public Seat Seat { get; set; }

        public int? id_client { get; set; } = null;
        public virtual Client Client { get; set; } = null;
    }
}
