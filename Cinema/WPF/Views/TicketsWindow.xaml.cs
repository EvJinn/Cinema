using System.Collections.Generic;
using System.Windows;
using Cinema.Models;
using Cinema.WPF.Models;

namespace Cinema.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для TicketsWindow.xaml
    /// </summary>
    public partial class TicketsWindow : Window
    {
        private Session _selectedSession;
        private List<Ticket> _listTickets;

        public TicketsWindow(Session selectedSession)
        {
            InitializeComponent();

            _selectedSession = selectedSession;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _listTickets = DataWorker.GetTicketsList(_selectedSession.id);

            TicketsList.ItemsSource = _listTickets;
        }

        private void CreateTicketButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewTicketWindow = new AddNewTicketWindow(_selectedSession);
            addNewTicketWindow.ShowDialog();

            _listTickets = DataWorker.GetTicketsList(_selectedSession.id);
            TicketsList.ItemsSource = _listTickets;
        }
    }
}
