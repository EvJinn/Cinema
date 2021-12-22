﻿using System;
using System.Collections.Generic;

namespace Cinema.Models
{
    public class Film
    {
        public int id { get; set; }
        public string Name { get; set; }
        public DateTime Duration { get; set; }

        public int id_AgeRestrict { get; set; }
        public AgeRestrict AgeRestrict { get; set; }

        public decimal Markup { get; set; }

        public List<FilmGenre> FilmGenre { get; set; } = new List<FilmGenre>();
        public List<Session> Session { get; set; } = new List<Session>();
    }
}
