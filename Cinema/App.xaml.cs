using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Cinema.Context;
using Cinema.MVVM.Models;
using Cinema.MVVM.ViewModels;
using Microsoft.Extensions.Configuration;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IConfiguration AppConfig { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ConfigurationBuilder().AddJsonFile("appConfig.json");
            AppConfig = builder.Build();

            DataWorker.AppConfig = AppConfig;

            var mainVM = new MainVM();

            var mainWindow = new MainWindow(AppConfig) { DataContext = mainVM };
            mainWindow.Show();
        }
    }
}
