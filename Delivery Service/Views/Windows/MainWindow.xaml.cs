using System;
using System.Windows;
using Delivery_Service.Data;
using Delivery_Service.Views.Pages.Auth;
using Delivery_Service.Data.Repositories;
using Delivery_Service.Model.Users;
using Delivery_Service.Model.Users.Roles.Admin;
using Delivery_Service.Services;

namespace Delivery_Service {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private IDataManager _dataManager = new DataManager(new AdminRepository(), new UserRepository(), new CourierRepository(), new DishRepository());

        public MainWindow() {
            InitializeComponent();
        }



        private void WindowLoaded(object sender, RoutedEventArgs e) {
            AuthFrame.NavigationService.Navigate(new SignInPage(_dataManager, new AuthService(_dataManager), this));

        }
        private void AuthFrame_ContentRendered(object sender, EventArgs e) {

        }

    }
}


