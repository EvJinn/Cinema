using System.Collections.Generic;

namespace Cinema.Models
{
    public class AgeRestrict
    {
        public int id { get; set; }
        public string Name { get; set; }

        public List<Film> Film { get; set; } = new List<Film>();
    }
}
