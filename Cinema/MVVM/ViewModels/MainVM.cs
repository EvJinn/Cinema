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
using System.Windows.Controls;
using System.Windows.Media;

namespace Cinema.MVVM.ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {
        #region DATA LISTS

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

        private List<object> _sessionsList = DataWorker.GetSessions();
        public List<object> SessionsList
        {
            get => _sessionsList;
            private set
            {
                _sessionsList = value;
                NotifyPropertyChanged("SessionsList");
            }
        }

        private List<object> _filmsList = DataWorker.GetFilms();
        public List<object> FilmsList
        {
            get => _filmsList;
            private set
            {
                _filmsList = value;
                NotifyPropertyChanged("FilmsList");
            }
        }

        #endregion



        #region WINDOWS DATA

        //Clients
        public static string ClientFirstName { get; set; }
        public static string ClientLastName { get; set; }
        public static string ClientPatronymic { get; set; }
        public static string ClientDiscount { get; set; }

        private void SetNullValuesToProperties()
        {
            //для клиента
            ClientFirstName = null;
            ClientLastName = null;
            ClientPatronymic = null;
            ClientDiscount = null;

        }

        private void UpdateClientsView()
        {
            ClientsList = DataWorker.GetClients();
            
            //MainWindow.Clients
        }
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

        #region WINDOWS METHODS

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
        private void SetRedBlockControl(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
        private void ShowMessageToUser(string message)
        {
            MessageWindow messageWnd = new MessageWindow(message);
            SetCenterPositionAndOpen(messageWnd);
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
                        Window wnd = obj as Window;
                        string resStr = "";
                        if (ClientFirstName == null || ClientFirstName.Replace(" ", "").Length == 0)
                            SetRedBlockControl(wnd, "FirstNameBlock");

                        if (ClientLastName == null || ClientLastName.Replace(" ", "").Length == 0)
                            SetRedBlockControl(wnd, "LastNameBlock");
                        else
                        {
                            resStr = DataWorker.AddClient(ClientFirstName, ClientLastName, ClientPatronymic, Convert.ToInt32(ClientDiscount));

                            ShowMessageToUser(resStr);
                            SetNullValuesToProperties();
                            wnd.Close();
                        }
                        
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
