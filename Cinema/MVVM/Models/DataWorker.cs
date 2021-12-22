using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.Context;
using Cinema.Models;

namespace Cinema.MVVM.Models
{
    public static class DataWorker
    {
        public static IConfiguration AppConfig;

        public static List<Client> GetClients()
        {
            using ApplicationContext db = new ApplicationContext(AppConfig);
            return db.Client.ToList();
        }

    }
}
