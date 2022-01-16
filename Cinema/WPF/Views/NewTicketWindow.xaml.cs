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
    /// Логика взаимодействия для NewTicketWindow.xaml
    /// </summary>
    public partial class NewTicketWindow : Window
    {
        private Session _selectedSession;
        private List<Ticket> _listTickets;

        public NewTicketWindow(Session selectedSession)
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

        }
    }
}
