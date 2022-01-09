using System.Collections.Generic;

namespace Cinema.Models
{
    /// <summary>
    /// Жанр
    /// </summary>
    public class Genre
    {
        public int id { get; set; }
        public string Name { get; set; }

        public List<FilmGenre> FilmGenre { get; set; } = new List<FilmGenre>();
    }
}
