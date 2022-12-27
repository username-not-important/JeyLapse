using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using JeyLapse.Annotations;
using JeyLapse.Session;

namespace JeyLapse.Operations
{
    public class PowerSaverAgent : INotifyPropertyChanged
    {
        private Visibility _blackScreenVisibility;
        private int _inactiveSeconds;
        private int _blackSeconds;

        public PowerSaverMode Mode { get; private set; }

        public Visibility BlackScreenVisibility
        {
            get { if (!IsEnabled) return Visibility.Collapsed; return _blackScreenVisibility; }
            private set
            {
                if (value == _blackScreenVisibility) return;
                _blackScreenVisibility = value;
                OnPropertyChanged("BlackScreenVisibility");
            }
        }

        public bool IsEnabled { get; set; }

        public PowerSaverAgent(PowerSaverMode mode)
        {
            Mode = mode;
            _inactiveSeconds = 0;
            _blackSeconds = 0;

            BlackScreenVisibility = Visibility.Collapsed;
        }

        public void Tick()
        {
            if (Mode == PowerSaverMode.Off)
                return;

            if (_blackScreenVisibility == Visibility.Collapsed)
                _inactiveSeconds++;
            else
                _blackSeconds++;

            Decide();
        }

        public void TouchBlack()
        {
            if (Mode == PowerSaverMode.Off)
                return;

            _blackSeconds = 0;
            _inactiveSeconds = 0;

            BlackScreenVisibility = Visibility.Collapsed;
        }

        public void Touch()
        {
            if (Mode == PowerSaverMode.Off)
                return;

            _inactiveSeconds = 0;
        }

        private void Decide()
        {
            if (Mode == PowerSaverMode.Off)
                return;
            else if (Mode == PowerSaverMode.Always)
            {
                if (_inactiveSeconds > 5)
                    BlackScreenVisibility = Visibility.Visible;
            }
            else if (Mode == PowerSaverMode.Interval)
            {
                if (_blackSeconds > 20)
                {
                    BlackScreenVisibility = Visibility.Collapsed;
                    _blackSeconds = 0;
                    _inactiveSeconds = 0;
                }

                if (_inactiveSeconds > 5)
                {
                    BlackScreenVisibility = Visibility.Visible;
                    _inactiveSeconds = 0;
                    _blackSeconds = 0;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
