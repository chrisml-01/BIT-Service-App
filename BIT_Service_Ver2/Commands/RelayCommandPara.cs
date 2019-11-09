using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BIT_Service_Ver2.Commands
{
    class RelayCommandPara<T> : ICommand
    {
        private Action<T> localAction;
        private bool _localCanExecute;

        public RelayCommandPara(Action<T> action, bool canExecute)
        {
            localAction = action;
            _localCanExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _localCanExecute;
        }

        public void Execute(Object parameter)
        {
            localAction((T)parameter);
        }
    }
}
