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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page2 page = new Page2("Press first button");
            this.NavigationService.Navigate(page);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Page2 page = new Page2("Press second button");
            this.NavigationService.Navigate(page);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Page2 page = new Page2("Press thrit button");
            this.NavigationService.Navigate(page);
        }
    }
}
