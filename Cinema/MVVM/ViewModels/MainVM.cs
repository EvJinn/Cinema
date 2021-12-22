using Cinema.Context;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System;
using Cinema.MVVM.Models;
using Cinema.MVVM.Views;
using System.Windows;

namespace Cinema.MVVM.ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {
        private List<Client> _clientsList = DataWorker.GetClients();
        public List<Client> ClientsList
        {
            get => _clientsList;
            private set
            {
                _clientsList = value;
                NotifyPropertyChanged("ClientsList");
            }
        }

        #region OPEN WINDOWS COMMANDS

        private RelayCommand _openAddNewClientWnd;
        public RelayCommand OpenAddNewClientWnd
        {
            get
            {
                return _openAddNewClientWnd ?? new RelayCommand(obj =>
                    {
                        OpenAddClientWindowMethod();
                    }
                );
            }
        }


        #endregion

        #region OPEN WINDOWS COMMANDS

        private void OpenAddClientWindowMethod()
        {
            AddNewClient newClientWindow = new AddNewClient();
            SetCenterPositionAndOpen(newClientWindow);
        }
        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
