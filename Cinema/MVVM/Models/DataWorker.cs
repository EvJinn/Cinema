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

        public static string AddClient(string firstname, string lastname, string patronymic, decimal discount)
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

        #region SESSIONS

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

        public static string DeleteSession(Session session)
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            db.Sessions.Remove(session);
            db.SaveChanges();

            return "Успешно!";
        }

        public static string AddSession(DateTime date, TimeSpan start, int id_hall, int id_film, decimal markup)
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            Session newSession = new Session
            {
                Date = date,
                Start = start,
                id_hall = id_hall,
                id_film = id_film,
                Markup = markup
            };
            db.Sessions.Add(newSession);
            db.SaveChanges();

            return "Успешно!";
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

        public static List<object> GetSeatsList()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            //return db.Seats.ToList();

            var seats = (from seat in db.Seats
                         join seatcategory in db.SeatCategories on seat.id_category equals seatcategory.id
                         join hall in db.Halls on seat.id_hall equals hall.id
                         select new
                         {
                             id = seat.id,
                             Number = seat.Number,
                             Row = seat.Row,
                             id_category = seat.id_category,
                             Category = seatcategory.Category,
                             id_hall = seat.id_hall,
                             Hall = hall.Name
                         }).ToList<object>();

            return seats;
        }

        #endregion
    }
}
