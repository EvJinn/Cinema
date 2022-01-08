using System.Windows;
using Cinema.WPF.Models;
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

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
