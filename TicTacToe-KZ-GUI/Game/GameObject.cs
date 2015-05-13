using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Game
{
    public class GameObject : INotifyPropertyChanged
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }    
        }

        private char _value;
        public char Value
        {
            get { return _value; }
            set
            {
                _isBusy = true;
                _value = value;
                OnPropertyChanged("Value");
            }
        }

        public GameObject()
        {
            _value = ' ';
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private TurnCommand _turnCommand;
        public TurnCommand TurnCommand
        {
            get { return _turnCommand ?? (_turnCommand = new TurnCommand(TextBlock_MouseDown)); }
        }

        private void TextBlock_MouseDown(object sender)
        {
            bool winner = false;
            try
            {
                winner = GameManager.SetValueAndCheckWinner(this);
                OnPropertyChanged("CurrentChar");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            if (winner)
                MessageBox.Show(string.Format("Выиграли {0}", GameManager.GetCurrentChar()));
        }
    }
}
