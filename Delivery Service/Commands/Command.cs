using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Delivery_Service.Commands {
    public class Command : ICommand {

        Action<object> _executemethod;
        Func<object, bool> _canexecutemethod;

        public Command(Action<object> executemethod, Func<object, bool> canexecutemethod) {
            _executemethod = executemethod;
            _canexecutemethod = canexecutemethod;
        }

        public bool CanExecute(object? parameter) {
            if (_canexecutemethod != null) {
                return _canexecutemethod(parameter);
            } else {
                return false;
            }
        }

        public event EventHandler? CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object? parameter) {
            _executemethod(parameter);
        }
    }
}
