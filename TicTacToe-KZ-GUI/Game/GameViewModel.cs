using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Game
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public GameObject[][] Field
        {
            get { return GameManager.GameField; }
        }

        public char CurrentChar
        {
            get { return GameManager.GetCurrentChar(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class TurnCommand : ICommand
    {
        private Action<object> execute;

        public TurnCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            execute(parameter);;
        }

        public event EventHandler CanExecuteChanged;
    }
}
