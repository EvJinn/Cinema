﻿using System;
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
using Cinema.WPF.Models;

namespace Cinema.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNewHallWindow.xaml
    /// </summary>
    public partial class AddNewHallWindow : Window
    {
        public AddNewHallWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text.Length == 0 || NameBox.Text.Replace(" ", "").Length == 0)
                NameBox.BorderBrush = Brushes.Red;

            else
            {
                string resStr = DataWorker.AddHall(NameBox.Text);
                MessageBox.Show(resStr, "Уведомление", MessageBoxButton.OK, MessageBoxImage.None);

                Close();
            }
        }
    }
}
