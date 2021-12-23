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
        #region DATA FOR BINDINGS

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

        private List<Hall> _hallsList = DataWorker.GetHalls();
        public List<Hall> HallsList
        {
            get => _hallsList;
            private set
            {
                _hallsList = value;
                NotifyPropertyChanged("HallsList");
            }
        }

        //public Hall SelectedHall { get; set; }

        private List<object> _seatsList = DataWorker.GetSeatsList();
        public List<object> SeatsList
        {
            get => _seatsList;
            private set
            {
                _seatsList = value;
                NotifyPropertyChanged("SeatsList");
            }
        }

        //private List<object> _filteredSessionsList = DataWorker.GetSessions();
        //public List<object> FilteredSessionsList
        //{
        //    get => _sessionsList;
        //    private set
        //    {
        //        _sessionsList = value;
        //        NotifyPropertyChanged("SessionsList");
        //    }
        //}

        //Фильтры в табе сеансов
        private bool _filterHallIsSelected;
        public bool FilterHallIsSelected
        {
            get => _filterHallIsSelected;
            private set
            {
                _filterHallIsSelected = value;
                NotifyPropertyChanged("FilterHallCheck");
            }
        }

        private bool _filterFilmIsSelected;
        public bool FilterFilmIsSelected
        {
            get => _filterFilmIsSelected;
            private set
            {
                _filterFilmIsSelected = value;
                NotifyPropertyChanged("FilterFilmCheck");
            }
        }

        private bool _filterDateIsSelected;
        public bool FilterDateIsSelected
        {
            get => _filterDateIsSelected;
            private set
            {
                _filterDateIsSelected = value;
                NotifyPropertyChanged("FilterDateCheck");
            }
        }

        public Hall SelectedHall { get; set; }

        #endregion



        #region WINDOWS DATA

        //Clients
        public static string ClientFirstName { get; set; }
        public static string ClientLastName { get; set; }
        public static string ClientPatronymic { get; set; }
        public static decimal ClientDiscount { get; set; }

        //Sessions
        public static DateTime SessionDate { get; set; }
        public static TimeSpan SessionStart { get; set; }
        public static Hall SessionHall { get; set; }
        public static Film SessionFilm { get; set; }
        public static decimal SessionMarkup { get; set; }

        private void SetNullValuesToProperties()
        {
            //для клиента
            ClientFirstName = null;
            ClientLastName = null;
            ClientPatronymic = null;
            ClientDiscount = 0;

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

        private RelayCommand _openAddNewSessionWnd;
        public RelayCommand OpenAddNewSessionWnd
        {
            get
            {
                return _openAddNewSessionWnd ?? new RelayCommand(obj =>
                    {
                        OpenAddSessionWindowMethod();
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
        private void OpenAddSessionWindowMethod()
        {
            AddNewSessionWindow newSessionWindow = new AddNewSessionWindow();
            SetCenterPositionAndOpen(newSessionWindow);
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
                            resStr = DataWorker.AddClient(ClientFirstName, ClientLastName, ClientPatronymic, ClientDiscount);

                            ShowMessageToUser(resStr);
                            SetNullValuesToProperties();
                            wnd.Close();
                        }
                    }
                );
            }
        }

        private RelayCommand _addNewSession;
        public RelayCommand AddNewSession
        {
            get
            {
                return _addNewSession ?? new RelayCommand(obj =>
                    {
                        Window wnd = obj as Window;
                        string resStr = "";

                        if (SessionHall == null)
                            SetRedBlockControl(wnd, "HallBox");

                        if (SessionFilm == null)
                            SetRedBlockControl(wnd, "FilmBox");

                        else
                        {
                            resStr = DataWorker.AddSession(SessionDate, SessionStart, SessionHall.id, SessionFilm.id, SessionMarkup);

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
