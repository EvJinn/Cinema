using System.Collections.Generic;

namespace Cinema.Models
{
    public class Genre
    {
        public int id { get; set; }
        public string Name { get; set; }

        public List<FilmGenre> FilmGenre { get; set; } = new List<FilmGenre>();
    }
}
