using Delivery_Service.Data;
using Delivery_Service.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Delivery_Service.Data.Repositories;
using Delivery_Service.Services;

namespace Delivery_Service.Views.Pages.Auth {
    /// <summary>
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page {
        IDataManager _dataManager;
        IAuthService _authService;
        Window _thisWindow;
        public SignUpPage(IDataManager dataManager, IAuthService authService, Window thisWindow) {
            InitializeComponent();
            _dataManager = dataManager;
            _authService = authService;
            _thisWindow = thisWindow;
            DataContext = new SignUpViewModel(_dataManager, _authService);
            _thisWindow = thisWindow;
        }

        private void HLinkLogin_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new SignInPage(_dataManager, _authService, _thisWindow));
            
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e) => Window.GetWindow(this).Close();

        private void Toolbar_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left) { Window.GetWindow(this).DragMove(); }
        }

    }
}
