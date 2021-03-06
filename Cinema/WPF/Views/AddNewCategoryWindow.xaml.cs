using System;
using System.Windows;
using System.Windows.Media;
using Cinema.WPF.Models;

namespace Cinema.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNewCategoryWindow.xaml
    /// </summary>
    public partial class AddNewCategoryWindow : Window
    {
        public AddNewCategoryWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Length == 0 || NameBox.Text.Replace(" ", "").Length == 0)
                NameBox.BorderBrush = Brushes.Red;

            if (!ValidateCost(CostBox.Text))
                CostBox.BorderBrush = Brushes.Red;

            else
            {
                string resStr = DataWorker.AddCategory(NameBox.Text, Convert.ToDecimal(CostBox.Text));
                MessageBox.Show(resStr, "Уведомление", MessageBoxButton.OK, MessageBoxImage.None);

                Close();
            }
        }

        private bool ValidateCost(string cost)
            => decimal.TryParse(cost, out decimal check);
    }
}
