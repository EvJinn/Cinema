﻿using System.Collections.Generic;
using System.Windows;
using System.Collections;
using Cinema.WPF.Models;
using System.Linq;
using Cinema.Models;
using Cinema.MVVM.Views;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Session> ListSessions;
        private List<Film> ListFilms;
        private List<Hall> ListHalls;

        public MainWindow()
        {
            InitializeComponent();

            ListFilms = DataWorker.GetFilms();
            ListHalls = DataWorker.GetHalls();

            ListSessions = DataWorker.GetSessions();
        }

        #region SESSION TAB

        private List<Session> FilteredSessionsList;

        private void Sessions_Loaded(object sender, RoutedEventArgs e)
        {
            SessionsList.ItemsSource = ListSessions;

            FilterFilmBox.ItemsSource = ListFilms;
            FilterHallBox.ItemsSource = ListHalls;

            FilteredSessionsList = ListSessions;
        }

        private void FilterDateCheck_Checked(object sender, RoutedEventArgs e)
        {
            FilteredSessionsList = FilteredSessionsList.Where(p => p.Date == FilterDateBox.SelectedDate).ToList();

            SessionsList.ItemsSource = FilteredSessionsList;
        }

        private void FilterDateCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            SessionsList.ItemsSource = ListSessions;
            FilteredSessionsList = ListSessions;

            FilterDateCheck.IsChecked = false;
            FilterFilmCheck.IsChecked = false;
            FilterHallCheck.IsChecked = false;
        }

        private void FilterHallCheck_Checked(object sender, RoutedEventArgs e)
        {
            Hall selectedHall = ListHalls.Find(p => p == FilterHallBox.SelectedItem);

            if (selectedHall != null)
            {
                FilteredSessionsList = FilteredSessionsList.Where(p => p.id_hall == selectedHall.id).ToList();

                SessionsList.ItemsSource = FilteredSessionsList;
            }
        }

        private void FilterHallCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            SessionsList.ItemsSource = ListSessions;
            FilteredSessionsList = ListSessions;

            FilterDateCheck.IsChecked = false;
            FilterFilmCheck.IsChecked = false;
            FilterHallCheck.IsChecked = false;
        }

        private void FilterFilmCheck_Checked(object sender, RoutedEventArgs e)
        {
            Film selectedFilm = ListFilms.Find(p => p == FilterFilmBox.SelectedItem);

            if (selectedFilm != null)
            {
                FilteredSessionsList = FilteredSessionsList.Where(p => p.id_film == selectedFilm.id).ToList();

                SessionsList.ItemsSource = FilteredSessionsList;
            }
        }

        private void FilterFilmCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            SessionsList.ItemsSource = ListSessions;
            FilteredSessionsList = ListSessions;

            FilterDateCheck.IsChecked = false;
            FilterFilmCheck.IsChecked = false;
            FilterHallCheck.IsChecked = false;
        }

        private void CreateSessionButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewSessionWindow = new AddNewSessionWindow(ListFilms, ListHalls);
            addNewSessionWindow.ShowDialog();

            ListSessions = DataWorker.GetSessions();
            SessionsList.ItemsSource = ListSessions;
        }

        #endregion

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Session SelectedSession = ListSessions.Find(p => p == SessionsList.SelectedItem);

            DataWorker.DeleteSession(SelectedSession);

            ListSessions = DataWorker.GetSessions();
            SessionsList.ItemsSource = ListSessions;
        }
    }
}
