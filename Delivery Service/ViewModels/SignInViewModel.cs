﻿using Delivery_Service.Services;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using Delivery_Service.Data;
using Delivery_Service.Commands;
using System.Windows.Navigation;
using Delivery_Service.Views.Windows;
using System;
using Delivery_Service.Utils;

namespace Delivery_Service.ViewModel {
    public class SignInViewModel : BaseViewModel {
        private IDataManager _dataManager;
        private IAuthService _authService;

        private string _role = "Admin";
        public string Role {
            get => _role;
            private set => _role = value;
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

        public ICommand LoginCommand { get; }
        public ICommand SetRoleCommand { get; }

        public SignInViewModel(IDataManager dataManager, IAuthService authService) {
            _dataManager = dataManager;
            _authService = authService;
            LoginCommand = new RelayCommand(Login);
            SetRoleCommand = new Command(SetRole, canexecutemethod => Role != null);
        }

        private void SetRole(object parameter) {
            Role = (string)parameter;
        }

        public event Action? AdminLogInSuccess;

        private void Login() {
            if (_authService.TrySignIn(_phone, _password, _role) && (_role == "Admin")) {
                AdminLogInSuccess?.Invoke();
                
            } else { MessageBox.Show("Проверьте корректность данных"); }
            
        }

    }
}
