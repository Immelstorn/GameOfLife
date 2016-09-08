using System.ComponentModel;
using System.Data;

namespace GameOfLife.Models
{
    public class MainModel : INotifyPropertyChanged
    {
        private int _number;
        private DataTable _Field;
        private int _x;
        private int _y;

        public event PropertyChangedEventHandler PropertyChanged;

        public int X
        {
            get { return _x; }
            set
            {
                if (value < 10)
                {
                    _x = 10;
                }
                else
                {
                    _x = value;
                }
            }
        }

        public int Y
        {
            get { return _y; }
            set
            {
                if (value < 10)
                {
                    _y = 10;
                }
                else
                {
                    _y = value;
                }
            }
        }

        public int Number
        {
            get
            {
                if (_number > X * Y)
                {
                    return X * Y;
                }
                return _number;
            }
            set { _number = value; }
        }

        public DataTable Field
        {
            get { return _Field; }
            set
            {
                _Field = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Field"));
                }
            }
        }

        public int Delay { get; set; }
    }
}
