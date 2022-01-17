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
    /// Логика взаимодействия для AddNewSeatWindow.xaml
    /// </summary>
    public partial class AddNewSeatWindow : Window
    {
        private Hall _selectedHall;
        private List<SeatCategory> _listSeatCategories;

        public AddNewSeatWindow(Hall selectedHall)
        {
            InitializeComponent();

            _selectedHall = selectedHall;

            _listSeatCategories = DataWorker.GetCategoryList();
            CategoryBox.ItemsSource = _listSeatCategories;
        }

        private List<Seat> _listSeats = new();
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryBox.SelectedItem == null)
                CategoryBox.BorderBrush = Brushes.Red;

            else
            {
                SeatCategory selectedCategory = _listSeatCategories.Find(x => x == CategoryBox.SelectedItem);

                Seat newSeat = new Seat
                {
                    Number = (int)NumberBox.Value,
                    Row = (int)RowBox.Value,
                    id_category = selectedCategory.id,
                    id_hall = _selectedHall.id,
                    SeatCategory = selectedCategory,
                };

                _listSeats.Add(newSeat);

                MessageBox.Show("Успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.None);

                NumberBox.Value = null;
                RowBox.Value = null;
                CategoryBox.SelectedItem = null;
                SeatsList.ItemsSource = null;
                SeatsList.ItemsSource = _listSeats;

                CreateButton.IsEnabled = true;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string resStr = DataWorker.AddSeats(_listSeats);

            MessageBox.Show(resStr, "Уведомление", MessageBoxButton.OK, MessageBoxImage.None);

            Close();
        }
    }
}
