using System.Collections.Generic;

namespace Cinema.Models
{
    public class FilmGenre
    {
        public int id_Film { get; set; }
        public Film Film { get; set; }

        public int id_Genre { get; set; }
        public Genre Genre { get; set; }
    }
}
