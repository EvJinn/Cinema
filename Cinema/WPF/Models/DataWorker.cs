using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Cinema.Context;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.WPF.Models
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

        public static string DeleteClient(Client client)
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            db.Client.Remove(client);
            db.SaveChanges();

            return "Успешно!";
        }

        #endregion

        #region SESSIONS

        public static List<Session> GetSessions()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            return db.Sessions.Include(e => e.Film).Include(e => e.Hall).ToList();
        }

        public static string AddSession(DateTime date, DateTimeOffset start, int id_hall, int id_film, decimal markup)
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

        public static List<Film> GetFilms()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);
            
            return db.Films.Include(e => e.AgeRestrict).ToList();
        }

        #endregion

        #region HALLS AND SEATS

        public static List<Hall> GetHalls()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            return db.Halls.ToList();
        }

        public static List<Seat> GetSeatsList()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            return db.Seats.Include(e => e.Hall).Include(e => e.SeatCategory).ToList();
        }

        public static List<SeatCategory> GetCategoryList()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            return db.SeatCategories.ToList();
        }
        #endregion
    }
}
