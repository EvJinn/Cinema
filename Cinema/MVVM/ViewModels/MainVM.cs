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

        #region WINDOWS DATA

        //Clients
        public static string ClientFirstName { get; set; }
        public static string ClientLastName { get; set; }
        public static string ClientPatronymic { get; set; }
        public static int ClientDiscount { get; set; }

        #endregion

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

        #region COMMANDS TO ADD

        private RelayCommand _addNewClient;
        public RelayCommand AddNewClient
        {
            get
            {
                return _addNewClient ?? new RelayCommand(obj =>
                    {
                        
                    }
                );
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
