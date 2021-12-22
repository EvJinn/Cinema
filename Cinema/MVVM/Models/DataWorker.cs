using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Cinema.Context;
using Cinema.Models;

namespace Cinema.MVVM.Models
{
    public static class DataWorker
    {
        public static IConfiguration AppConfig;

        #region CLIENTS

        public static List<Client> GetClients()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            return db.Client.ToList();
        }

        public static string AddClient(string firstname, string lastname, string patronymic, int discount)
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            Client newClient = new Client
            {
                FirstName = firstname,
                LastName = lastname,
                Patronymic = patronymic,
                Discount = discount
            };
            db.Client.Add(newClient);
            db.SaveChanges();

            return "Успешно!";
        }

        #endregion

        #region TICKETS

        public static List<object> GetSessions()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            //return db.Sessions.ToList();
            var sessions = (from session in db.Sessions
                           join hall in db.Halls on session.id_hall equals hall.id
                           join film in db.Films on session.id_film equals film.id
                           select new
                           {
                               id = session.id,
                               Date = session.Date,
                               Start = session.Start,
                               Markup = session.Markup,
                               id_hall = session.id_hall,
                               Hall = hall.Name,
                               id_film = session.id_film,
                               Film = film.Name,
                           }).ToList<object>();

            return sessions;
        }

        #endregion

        #region FILMS

        public static List<object> GetFilms()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);
            
            //return db.Films.ToList();
            var films = (from film in db.Films
                join agerestrict in db.AgeRestricts on film.id_agerestrict equals agerestrict.id
                select new
                {
                    id = film.id,
                    Name = film.Name,
                    Duration = film.Duration,
                    id_agerestrict = film.id_agerestrict,
                    Markup = film.Markup,
                    AgeRestrict = agerestrict.Name,
                }).ToList<object>();

            return films;
        }

        #endregion

        #region HALLS AND SEATS

        public static List<Hall> GetHalls()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            return db.Halls.ToList();
        }

        #endregion
    }
}
