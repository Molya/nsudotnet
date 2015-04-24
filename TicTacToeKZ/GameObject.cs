using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameConsole
{
    public class GameObject
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
            }
        }

        public GameObject()
        {
            _value = ' ';
        }
    }
}
