using System.Collections.Generic;

namespace Cinema.Models
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class Client
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public decimal Discount { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
