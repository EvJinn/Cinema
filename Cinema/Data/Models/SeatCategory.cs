using System.Collections.Generic;

namespace Cinema.Models
{
    /// <summary>
    /// Категория места
    /// </summary>
    public class SeatCategory
    {
        public int id { get; set; }
        public string Category { get; set; }
        public decimal Cost { get; set; }

        public List<Seat> Seat { get; set; } = new List<Seat>();
    }
}
