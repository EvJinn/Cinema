using System;
using System.Windows;
using System.Windows.Media;
using Cinema.WPF.Models;

namespace Cinema.MVVM.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNewClient.xaml
    /// </summary>
    public partial class AddNewClient : Window
    {
        public AddNewClient()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string resStr = "";

            if (FirstNameBox.Text.Length == 0 || FirstNameBox.Text.Replace(" ", "").Length == 0)
                FirstNameBox.BorderBrush = Brushes.Red;

            if (LastNameBox.Text.Length == 0 || LastNameBox.Text.Replace(" ", "").Length == 0)
                LastNameBox.BorderBrush = Brushes.Red;

            //if (PatronymicBox.Text == null || PatronymicBox.Text.Replace(" ", "").Length == 0)
            //    PatronymicBox.BorderBrush = Brushes.Red;

            if (DiscountBox.SelectedItem == null)
                DiscountBox.BorderBrush = Brushes.Red;

            else
            {
                resStr = DataWorker.AddClient(FirstNameBox.Text, LastNameBox.Text, PatronymicBox.Text,
                    Convert.ToDecimal(DiscountBox.SelectionBoxItem));

                MessageBox.Show(resStr, "Уведомление", MessageBoxButton.OK, MessageBoxImage.None);

                this.Close();
            }
        }
    }
}
