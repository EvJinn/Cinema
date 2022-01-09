using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Cinema.Models;
using Cinema.WPF.Models;

namespace Cinema.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNewFilmWindow.xaml
    /// </summary>
    public partial class AddNewFilmWindow : Window
    {
        private List<AgeRestrict> ListAgeRestricts;

        public AddNewFilmWindow(List<AgeRestrict> listAgeRestricts)
        {
            InitializeComponent();

            ListAgeRestricts = listAgeRestricts;

            AgeRestrictBox.ItemsSource = listAgeRestricts;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string resStr = "";

            if (NameBox.Text.Length == 0 || NameBox.Text.Replace(" ", "").Length == 0)
                NameBox.BorderBrush = Brushes.Red;

            if (AgeRestrictBox.SelectedItem == null)
                AgeRestrictBox.BorderBrush = Brushes.Red;

            if (!ValidateMarkup(MarkupBox.Text))
                MarkupBox.BorderBrush = Brushes.Red;

            else
            {
                AgeRestrict selectedAgeRestrict = ListAgeRestricts.Find(p => p == AgeRestrictBox.SelectedItem);

                resStr = DataWorker.AddFilm(NameBox.Text, TimeSpan.Parse(DurationBox.Text), selectedAgeRestrict.id, Convert.ToDecimal(MarkupBox.Text));

                MessageBox.Show(resStr, "Уведомление", MessageBoxButton.OK, MessageBoxImage.None);

                this.Close();
            }
        }

        private bool ValidateMarkup(string markup)
            => decimal.TryParse(markup, out decimal check);
    }
}
