using System.Collections.Generic;

namespace Cinema.Models
{
    /// <summary>
    /// Возрастное ограничение
    /// </summary>
    public class AgeRestrict
    {
        public int id { get; set; }
        public string Name { get; set; }

        public List<Film> Film { get; set; } = new List<Film>();
    }
}
