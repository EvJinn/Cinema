using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Cinema.Context;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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
            using ApplicationContext db = new(AppConfig);

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
            using ApplicationContext db = new(AppConfig);

            try
            {
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
            catch (Exception e)
            {
                return e.Message;
            }
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
            using ApplicationContext db = new(AppConfig);

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
            using ApplicationContext db = new(AppConfig);

            try
            {
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
            catch (Exception e)
            {
                return e.Message;
            }
        }

        #endregion

        #region TICKETS

        public static List<Ticket> GetTicketsList(int id_session)
        {
            using ApplicationContext db = new(AppConfig);

            return db.Tickets.Where(p => p.id_session == id_session).Include(e => e.Client).Include(e => e.Seat)
                .Include(e => e.Session).ToList();
        }

        public static string AddTicket(List<Ticket> tickets)
        {
            using ApplicationContext db = new(AppConfig);
            using IDbContextTransaction transaction = db.Database.BeginTransaction();

            try
            {
                foreach (var ticket in tickets)
                {
                    Ticket newTicket = new()
                    {
                        id_client = ticket.id_client,
                        id_seat = ticket.id_seat,
                        id_session = ticket.id_session,
                        Cost = ticket.Cost,
                    };

                    db.Tickets.Add(newTicket);
                }
                db.SaveChanges();
                transaction.Commit();

                return "Успешно";
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return e.Message;
            }
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
            using ApplicationContext db = new(AppConfig);
            
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
            using ApplicationContext db = new(AppConfig);

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
            using ApplicationContext db = new(AppConfig);

            try
            {
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
            catch (Exception e)
            {
                return e.Message;
            }
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
            using ApplicationContext db = new(AppConfig);

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
            using ApplicationContext db = new(AppConfig);

            return db.Seats.Include(e => e.Hall).Include(e => e.SeatCategory).ToList();
        }

        /// <summary>
        /// Получить список мест в зале из базы данных
        /// </summary>
        /// <returns>
        /// Список мест в зале
        /// </returns>
        /// <param name="id_hall">Зал</param>
        public static List<Seat> GetSeatsList(int id_hall)
        {
            using ApplicationContext db = new(AppConfig);

            return db.Seats.Where(p => p.id_hall == id_hall).Include(e => e.SeatCategory).ToList();
        }

        /// <summary>
        /// Получить список категорий мест из базы данных
        /// </summary>
        /// <returns>
        /// Список категорий мест
        /// </returns>
        public static List<SeatCategory> GetCategoryList()
        {
            using ApplicationContext db = new(AppConfig);

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
            using ApplicationContext db = new(AppConfig);

            try
            {
                Hall newHall = new Hall
                {
                    Name = name
                };
                db.Halls.Add(newHall);
                db.SaveChanges();

                return "Успешно";
            }
            catch (Exception e)
            {
                return e.Message;
            }
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
            using ApplicationContext db = new(AppConfig);

            try
            {
                SeatCategory newCategory = new SeatCategory
                {
                    Category = name,
                    Cost = cost
                };
                db.SeatCategories.Add(newCategory);
                db.SaveChanges();

                return "Успешно";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string AddSeats(List<Seat> seats)
        {
            using ApplicationContext db = new(AppConfig);
            using IDbContextTransaction transaction = db.Database.BeginTransaction();

            try
            {
                foreach (var seat in seats)
                {
                    Seat newSeat = new()
                    {
                        id_hall = seat.id_hall,
                        id_category = seat.id_category,
                        Number = seat.Number,
                        Row = seat.Row
                    };

                    db.Seats.Add(newSeat);
                }
                db.SaveChanges();
                transaction.Commit();

                return "Успешно";
            }
            catch (Exception e)
            {
                transaction.Rollback();
                return e.Message;
            }
        }
        #endregion

        #region REPORTS

        public static List<object> GetAmountSessionByDateReport(DateTime? start, DateTime? end)
        {
            using ApplicationContext db = new(AppConfig);

            var res = (from session in db.Sessions
                where (session.Date >= start) && (session.Date <= end)
                group session by session.Date
                into grp
                select new
                {
                    Date = grp.Key,
                    Count = grp.Select(x => x.id).Count()
                }).OrderBy(x => x.Date).ToList<object>();

            return res;
        }

        public static int GetAmountSessionReport(DateTime? start, DateTime? end)
        {
            using ApplicationContext db = new(AppConfig);

            return db.Sessions.Where(x => (x.Date >= start) && (x.Date <= end)).Count();
        }

        public static List<object> GetAmountTicketsByDateReport(DateTime? start, DateTime? end)
        {
            using ApplicationContext db = new(AppConfig);

            var res = (from ticket in db.Tickets
                    join session in db.Sessions on ticket.id_session equals session.id
                    where (session.Date >= start) && (session.Date <= end)
                    group ticket by session.Date into grp
                    select new
                    {
                        Date = grp.Key,
                        Count = grp.Select(x => x.id).Count(),
                        Sum = grp.Sum(x => x.Cost),
                    }).OrderBy(x => x.Date).ToList<object>();

            return res;
        }

        public static (int, decimal) GetAmountTicketsReport(DateTime? start, DateTime? end)
        {
            using ApplicationContext db = new(AppConfig);

            var count = db.Tickets.Include(e => e.Session)
                .Where(x => (x.Session.Date >= start) && (x.Session.Date <= end)).Count();

            var sum = db.Tickets.Include(e => e.Session)
                .Where(x => (x.Session.Date >= start) && (x.Session.Date <= end)).Sum(x => x.Cost);

            return (count, sum);
        }

        public static string GetPopularFilmReport(DateTime? start, DateTime? end)
        {
            try
            {
                using ApplicationContext db = new(AppConfig);

                var res = (from ticket in db.Tickets
                    join session in db.Sessions on ticket.id_session equals session.id
                    join film in db.Films on session.id_film equals film.id
                    where (session.Date >= start) && (session.Date <= end)
                    group ticket by film.Name
                    into grp
                    select new
                    {
                        fName = grp.Key,
                        Count = grp.Select(x => x.id).Count(),
                    }).OrderByDescending(x => x.Count).Take(1).Select(x => x.fName).ToList();

                return res[0];
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
        }

        #endregion
    }
}
