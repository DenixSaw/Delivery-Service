using Delivery_Service.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Input;
using Delivery_Service.Data;
using Delivery_Service.Services;
using Delivery_Service.Views.Windows;

namespace Delivery_Service.Views.Pages.Auth {
    /// <summary>
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page {

        private IDataManager _dataManager;
        private IAuthService _authService;
        private Window _thisWindow;
        public SignInPage(IDataManager dataManager, IAuthService authService, Window thisWindow) {
            _dataManager = dataManager;
            _authService = authService;
            _thisWindow = thisWindow;
            InitializeComponent();
            DataContext = new SignInViewModel(_dataManager, _authService);
            if (DataContext is SignInViewModel signInViewModel) {
                signInViewModel.AdminLogInSuccess += OpenAdminWindow;
            }
        }

        private void OpenAdminWindow() {
            AdminWindow window = new AdminWindow(_dataManager);
            window.Show();
            _thisWindow.Close();
        }

        private void HLinkRegistration_Click(object sender, RoutedEventArgs e) {
            this.NavigationService.Navigate(new SignUpPage(_dataManager, _authService, _thisWindow));
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e) => Window.GetWindow(this).Close();

        private void Toolbar_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.ChangedButton == MouseButton.Left) { Window.GetWindow(this).DragMove(); }
        }
    }
}
