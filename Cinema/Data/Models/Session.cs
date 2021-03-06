using System;
using System.Collections.Generic;

namespace Cinema.Models
{
    /// <summary>
    /// Сеанс
    /// </summary>
    public class Session
    {
        public int id { get; set; }
        public DateTime Date { get; set; }
        public DateTimeOffset Start { get; set; }

        public int id_hall { get; set; }
        public Hall Hall { get; set; }

        public decimal Markup { get; set; }

        public int id_film { get; set; }
        public Film Film { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
