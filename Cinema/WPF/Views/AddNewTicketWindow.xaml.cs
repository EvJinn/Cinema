using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
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
        private List<Ticket> _listTickets;
        private List<Seat> _filteredListSeats;

        public AddNewTicketWindow(Session selectedSession, List<Ticket> listTickets)
        {
            InitializeComponent();

            _selectedSession = selectedSession;
            _listTickets = listTickets;

            _listClients = DataWorker.GetClients();
            _listSeats = DataWorker.GetSeatsList(_selectedSession.id_hall);

            _filteredListSeats = _listSeats;
            foreach (var i in _listTickets)
            {
                _filteredListSeats.RemoveAll(x => x.id == i.Seat.id);
            }

            ClientBox.ItemsSource = _listClients;
            SeatBox.ItemsSource = _filteredListSeats;
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
                AddButton.IsEnabled = true;
            }
            else
                SeatBox.BorderBrush = Brushes.Red;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Seat selectedSeat = _listSeats.Find(p => p == SeatBox.SelectedItem);
            Client selectedClient = _listClients.Find(p => p == ClientBox.SelectedItem);

            string resStr;
            if (selectedClient != null)
            {
                Ticket ticket = new Ticket
                {
                    id_client = selectedClient.id,
                    id_session = _selectedSession.id,
                    Cost = Convert.ToDecimal(CostBox.Text),
                    id_seat = selectedSeat.id,
                    Seat = selectedSeat
                };

                _newTickets.Add(ticket);
            }
            //resStr = DataWorker.AddTicket(selectedClient.id, _selectedSession.id, Convert.ToDecimal(CostBox.Text), selectedSeat.id);
            else
            {
                Ticket ticket = new Ticket
                {
                    id_client = null,
                    id_session = _selectedSession.id,
                    Cost = Convert.ToDecimal(CostBox.Text),
                    id_seat = selectedSeat.id,
                    Seat = selectedSeat
                };

                _newTickets.Add(ticket);
            }
            //resStr = DataWorker.AddTicket(null, _selectedSession.id, Convert.ToDecimal(CostBox.Text), selectedSeat.id);

            MessageBox.Show("Успешно", "Уведомление", MessageBoxButton.OK, MessageBoxImage.None);

            AddButton.IsEnabled = false;
            CreateButton.IsEnabled = true;

            TicketsList.ItemsSource = null;
            TicketsList.ItemsSource = _newTickets;

            SeatBox.SelectedItem = null;
            ClientBox.SelectedItem = null;
            CostBox.Text = "";

            //this.Close();
        }

        private List<Ticket> _newTickets = new();

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string resStr = DataWorker.AddTicket(_newTickets);

            MessageBox.Show(resStr, "Уведомление", MessageBoxButton.OK, MessageBoxImage.None);

            Close();
        }
    }
}
