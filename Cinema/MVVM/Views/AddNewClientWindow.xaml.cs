using System.Windows;
using Cinema.MVVM.ViewModels;

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
            DataContext = new MainVM();
        }
    }
}
