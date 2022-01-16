using System;
using System.Collections.Generic;
using System.Windows;
using Cinema.Models;
using Cinema.WPF.Models;

namespace Cinema.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNewTicketWindow.xaml
    /// </summary>
    public partial class AddNewTicketWindow : Window
    {
        private Session _selectedSession;
        private List<Client> _listClients;
        private List<Seat> _listSeats;

        public AddNewTicketWindow(Session selectedSession)
        {
            InitializeComponent();

            _selectedSession = selectedSession;

            _listClients = DataWorker.GetClients();
            _listSeats = DataWorker.GetSeatsList(_selectedSession.id_hall);

            ClientBox.ItemsSource = _listClients;
            SeatBox.ItemsSource = _listSeats;
        }

        private void CheckClient_Checked(object sender, RoutedEventArgs e)
            => ClientBox.IsEnabled = true;

        private void CheckClient_Unchecked(object sender, RoutedEventArgs e)
        {
            ClientBox.IsEnabled = false;
            ClientBox.SelectedItem = null;
        }

        private void CalculateCostButton_Click(object sender, RoutedEventArgs e)
        {
            CostBox.IsEnabled = true;

            decimal cost;

            if (SeatBox.SelectedItem != null)
            {
                Seat selectedSeat = _listSeats.Find(p => p == SeatBox.SelectedItem);

                if (ClientBox.SelectedItem != null)
                {
                    Client selectedClient = _listClients.Find(p => p == ClientBox.SelectedItem);

                    cost = (selectedSeat.SeatCategory.Cost + _selectedSession.Markup + _selectedSession.Film.Markup) - (selectedSeat.SeatCategory.Cost + _selectedSession.Markup + _selectedSession.Film.Markup) * selectedClient.Discount * (decimal)0.01;
                }
                else
                    cost = selectedSeat.SeatCategory.Cost + _selectedSession.Markup + _selectedSession.Film.Markup;

                CostBox.Text = cost.ToString();
                CreateButton.IsEnabled = true;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Seat selectedSeat = _listSeats.Find(p => p == SeatBox.SelectedItem);
            Client selectedClient = _listClients.Find(p => p == ClientBox.SelectedItem);

            string resStr;
            if (selectedClient != null)
                resStr = DataWorker.AddTicket(selectedClient.id, _selectedSession.id, Convert.ToDecimal(CostBox.Text), selectedSeat.id);
            else
                resStr = DataWorker.AddTicket(null, _selectedSession.id, Convert.ToDecimal(CostBox.Text), selectedSeat.id);

            MessageBox.Show(resStr, "Уведомление", MessageBoxButton.OK, MessageBoxImage.None);

            this.Close();
        }
    }
}
