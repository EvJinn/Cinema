using System.Windows;
using Cinema.MVVM.ViewModels;

namespace Cinema.MVVM.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNewSessionWindow.xaml
    /// </summary>
    public partial class AddNewSessionWindow : Window
    {
        public AddNewSessionWindow()
        {
            InitializeComponent();
            DataContext = new MainVM();
        }
    }
}
