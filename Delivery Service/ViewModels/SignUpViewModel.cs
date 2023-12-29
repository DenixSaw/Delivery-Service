using Delivery_Service.Commands;
using Delivery_Service.Data;
using Delivery_Service.Model;
using Delivery_Service.Services;
using Delivery_Service.Views.Pages.Auth;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace Delivery_Service.ViewModel {

    public class SignUpViewModel : BaseViewModel {
        private string _role = "Admin";
        public string Role {
            get => _role;
            private set => _role = value;
        }

        private IDataManager _dataManager;
        private IAuthService _authService;

        private string _userName = "";
        public string UserName {
            get => _userName;
            set {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _phone = "";
        public string Phone {
            get => _phone;
            set {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private string _password = "";
        public string Password {
            get => _password;
            set {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _repeatedPassword = "";
        public string RepeatedPassword {
            get => _repeatedPassword;
            set {
                _repeatedPassword = value;
                OnPropertyChanged(nameof(RepeatedPassword));
            }
        }

        public ICommand RegistrationCommand { get; }

        public ICommand SetRoleCommand { get; }

        public SignUpViewModel(IDataManager dataManager, IAuthService authService) {
            _dataManager = dataManager;
            _authService = authService;
            RegistrationCommand = new RelayCommand(Registration);
            SetRoleCommand = new Command(SetRole, canexecutemethod => Role != null);
        }

        private void SetRole(object parameter) {
            Role = (string)parameter;
        }

        private void Registration() {
            Regex phoneRegex = new(@"^[0-9]{10}$");
            Regex passwordRegex = new(@"^[a-zA-Z0-9]{6,}$");
            string errorMessage = "";
            if (!phoneRegex.IsMatch(Phone))
                errorMessage += "Проверьте корректность телефона\n";
            if (UserName.Length == 0)
                errorMessage += "Поле ФИО не может быть пустым\n";
            if (!passwordRegex.IsMatch(Password))
                errorMessage += "Пароль должен иметь длину не менее 6 и содержать только буквы английского алфавита и цифры\n";
            if (Password != RepeatedPassword)
                errorMessage += "Пароли не совпадают\n";

            if (errorMessage.Length > 0)
                MessageBox.Show(errorMessage);
            else {
                if(_authService.TrySignUp(UserName,Phone,Password,RepeatedPassword, Role)) {
                    MessageBox.Show("Вы успешно зарегистрировались");

                }
            }

        }
    }
}
