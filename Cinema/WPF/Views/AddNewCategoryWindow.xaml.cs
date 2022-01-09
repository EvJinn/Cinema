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
            string resStr = "";

            if (NameBox.Text.Length == 0 || NameBox.Text.Replace(" ", "").Length == 0)
                NameBox.BorderBrush = Brushes.Red;

            if (!ValidateCost(CostBox.Text))
                CostBox.BorderBrush = Brushes.Red;

            else
            {
                resStr = DataWorker.AddCategory(NameBox.Text, Convert.ToDecimal(CostBox.Text));

                MessageBox.Show(resStr, "Уведомление", MessageBoxButton.OK, MessageBoxImage.None);

                this.Close();
            }
        }

        private bool ValidateCost(string cost)
            => decimal.TryParse(cost, out decimal check);
    }
}
