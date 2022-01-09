using System.Collections.Generic;

namespace Cinema.Models
{
    /// <summary>
    /// Связь фильмов с жанрами
    /// </summary>
    public class FilmGenre
    {
        public int id_Film { get; set; }
        public Film Film { get; set; }

        public int id_Genre { get; set; }
        public Genre Genre { get; set; }
    }
}
