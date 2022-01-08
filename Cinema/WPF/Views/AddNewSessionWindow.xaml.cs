using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Cinema.Models;
using Cinema.WPF.Models;

namespace Cinema.MVVM.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNewSessionWindow.xaml
    /// </summary>
    public partial class AddNewSessionWindow : Window
    {
        private List<Film> ListFilms;
        private List<Hall> ListHalls;

        public AddNewSessionWindow(List<Film> listFilms, List<Hall> listHalls)
        {
            InitializeComponent();

            ListFilms = listFilms;
            ListHalls = listHalls;

            HallBox.ItemsSource = listHalls;
            FilmBox.ItemsSource = listFilms;
        }

        private void AddNewSessionWnd_Loaded(object sender, RoutedEventArgs e)
        {
            DateBox.SelectedDate = DateTime.Now;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string resStr = "";

            if (HallBox.SelectedItem == null)
                HallBox.BorderBrush = Brushes.Red;

            if (FilmBox.SelectedItem == null)
                FilmBox.BorderBrush = Brushes.Red;

            if (!ValidateMarkup(MarkupBox.Text))
                MarkupBox.BorderBrush = Brushes.Red;

            else
            {
                Hall selectedHall = ListHalls.Find(p => p == HallBox.SelectedItem);
                Film selectedFilm = ListFilms.Find(p => p == FilmBox.SelectedItem);

                DateTime date = DateBox.SelectedDate ?? DateTime.Now;

                resStr = DataWorker.AddSession(date, (DateTimeOffset)StartBox.Value, selectedHall.id, selectedFilm.id, Convert.ToDecimal(MarkupBox.Text));

                MessageBox.Show(resStr, "Уведомление", MessageBoxButton.OK, MessageBoxImage.None);
                
                this.Close();
            }
        }

        private bool ValidateMarkup(string markup)
            => decimal.TryParse(markup, out decimal check);

        private bool ValidateDateTimeOffset(string timeSpan)
            => DateTimeOffset.TryParse(timeSpan, out DateTimeOffset check);
    }
}
