using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Cinema.Models;
using Cinema.WPF.Models;

namespace Cinema.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNewFilmWindow.xaml
    /// </summary>
    public partial class AddNewFilmWindow : Window
    {
        private List<AgeRestrict> _listAgeRestricts;

        public AddNewFilmWindow(List<AgeRestrict> listAgeRestricts)
        {
            InitializeComponent();

            _listAgeRestricts = listAgeRestricts;

            AgeRestrictBox.ItemsSource = _listAgeRestricts;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Length == 0 || NameBox.Text.Replace(" ", "").Length == 0)
                NameBox.BorderBrush = Brushes.Red;

            if (AgeRestrictBox.SelectedItem == null)
                AgeRestrictBox.BorderBrush = Brushes.Red;

            if (!ValidateMarkup(MarkupBox.Text))
                MarkupBox.BorderBrush = Brushes.Red;

            else
            {
                AgeRestrict selectedAgeRestrict = _listAgeRestricts.Find(p => p == AgeRestrictBox.SelectedItem);

                string resStr = DataWorker.AddFilm(NameBox.Text, TimeSpan.Parse(DurationBox.Text), selectedAgeRestrict.id, Convert.ToDecimal(MarkupBox.Text));

                MessageBox.Show(resStr, "Уведомление", MessageBoxButton.OK, MessageBoxImage.None);

                Close();
            }
        }

        private bool ValidateMarkup(string markup)
            => decimal.TryParse(markup, out decimal check);
    }
}
