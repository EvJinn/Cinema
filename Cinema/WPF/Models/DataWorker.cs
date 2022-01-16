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

        /// <summary>
        /// Получить список клиентов из базы данных
        /// </summary>
        /// <returns>
        /// Список клиентов
        /// </returns>
        public static List<Client> GetClients()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            return db.Client.ToList();
        }

        /// <summary>
        /// Добавить нового клиента в базу данных
        /// </summary>
        /// <param name="firstname">Фамилия клиента</param>
        /// <param name="lastname">Имя клиента</param>
        /// <param name="patronymic">Отчество клиента</param>
        /// <param name="discount">Скидка</param>
        /// <returns>
        /// Строка результата
        /// </returns>
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

        /// <summary>
        /// Получить список сеансов из базы данных
        /// </summary>
        /// <returns>
        /// Список сеансов
        /// </returns>
        public static List<Session> GetSessions()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            return db.Sessions.Include(e => e.Film).Include(e => e.Hall).ToList();
        }

        /// <summary>
        /// Добавить новый сеанс в базу данных
        /// </summary>
        /// <param name="date">Дата проведения</param>
        /// <param name="start">Время начала</param>
        /// <param name="id_hall">Зал проведения</param>
        /// <param name="id_film">Фильм</param>
        /// <param name="markup">Наценка за сеанс</param>
        /// <returns>
        /// Строка результата
        /// </returns>
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

        #region TICKETS

        public static List<Ticket> GetTicketsList(int id_session)
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            return db.Tickets.Where(p => p.id_session == id_session).Include(e => e.Client).Include(e => e.Seat)
                .Include(e => e.Session).ToList();
        }

        #endregion

        #region FILMS

        /// <summary>
        /// Получить список фильмов из базы данных
        /// </summary>
        /// <returns>
        /// Список фильмов
        /// </returns>
        public static List<Film> GetFilms()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);
            
            return db.Films.Include(e => e.AgeRestrict).ToList();
        }

        /// <summary>
        /// Получить список возрастных ограничений из базы данных
        /// </summary>
        /// <returns>
        /// Список возрастных ограничений
        /// </returns>
        public static List<AgeRestrict> GetAgeRestricts()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            return db.AgeRestricts.ToList();
        }

        /// <summary>
        /// Добавить новый фильм в базу данных
        /// </summary>
        /// <param name="name">Название фильма</param>
        /// <param name="duration">Длительность фильма</param>
        /// <param name="id_agerestrict">Возрастное ограничение</param>
        /// <param name="markup">Наценка за фильм</param>
        /// <returns>
        /// Строка результата
        /// </returns>
        public static string AddFilm(string name, TimeSpan duration, int id_agerestrict, decimal markup)
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            Film newFilm = new Film
            {
                Name = name,
                Duration = duration,
                id_agerestrict = id_agerestrict,
                Markup = markup
            };
            db.Films.Add(newFilm);
            db.SaveChanges();

            return "Успешно";
        }

        #endregion

        #region HALLS AND SEATS

        /// <summary>
        /// Получить список залов из базы данных
        /// </summary>
        /// <returns>
        /// Список залов
        /// </returns>
        public static List<Hall> GetHalls()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            return db.Halls.ToList();
        }

        /// <summary>
        /// Получить список мест из базы данных
        /// </summary>
        /// <returns>
        /// Список мест
        /// </returns>
        public static List<Seat> GetSeatsList()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            return db.Seats.Include(e => e.Hall).Include(e => e.SeatCategory).ToList();
        }

        /// <summary>
        /// Получить список категорий мест из базы данных
        /// </summary>
        /// <returns>
        /// Список категорий мест
        /// </returns>
        public static List<SeatCategory> GetCategoryList()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            return db.SeatCategories.ToList();
        }

        /// <summary>
        /// Добавить новый зал в базу данных
        /// </summary>
        /// <param name="name">Название зала</param>
        /// <returns>
        /// Строка результата
        /// </returns>
        public static string AddHall(string name)
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            Hall newHall = new Hall
            {
                Name = name
            };
            db.Halls.Add(newHall);
            db.SaveChanges();

            return "Успешно";
        }

        /// <summary>
        /// Добавить новую категрию в базу данных
        /// </summary>
        /// <param name="name">Название категории</param>
        /// <param name="cost">Стоимость места данной категории</param>
        /// <returns>
        /// Строка результата
        /// </returns>
        public static string AddCategory(string name, decimal cost)
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);

            SeatCategory newCategory = new SeatCategory
            {
                Category = name,
                Cost = cost
            };
            db.SeatCategories.Add(newCategory);
            db.SaveChanges();

            return "Успешно";
        }
        #endregion
    }
}
