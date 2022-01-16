using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using Cinema.Models;

namespace Cinema.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для SeatsInHallWindow.xaml
    /// </summary>
    public partial class SeatsInHallWindow : Window
    {
        private List<Seat> _listSeats;

        public SeatsInHallWindow(List<Seat> listSeats)
        {
            InitializeComponent();

            _listSeats = listSeats;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int rows = _listSeats.Max(t => t.Row);
            int numbers = _listSeats.Max(t => t.Number);

            var dt = new DataTable();

            for (int i = 0; i < rows; i++)
            {
                dt.Columns.Add(Convert.ToString(i+1));
            }

            for (int i = 0; i < numbers; i++)
            {
                dt.Rows.Add();
            }

            SeatsGrid.ItemsSource = dt.AsDataView();
        }
    }
}
