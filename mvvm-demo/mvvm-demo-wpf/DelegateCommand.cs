using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace mvvm_demo_wpf
{
    public class DelegateCommand : ICommand
    {
        private Action _action;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }


        public DelegateCommand(Action action)
        {
            _action = action;
        }


#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning restore 67
    }
}
