﻿using Delivery_Service.Data;
using Delivery_Service.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Delivery_Service.Views.Pages.Admin {
    /// <summary>
    /// Логика взаимодействия для AddOrderPage.xaml
    /// </summary>
    public partial class AddOrderPage : Page {
        private IDataManager _datamanager;
        public AddOrderPage(IDataManager dataManager) {
            _datamanager = dataManager;
            InitializeComponent();
            DataContext = new AddOrderViewModel(_datamanager);
        }

        private void ListOrderPage(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new ListOrderPage(_datamanager));

        }

        private void ListDishesPage(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new ListDishPage(_datamanager));
        }
    }


}
