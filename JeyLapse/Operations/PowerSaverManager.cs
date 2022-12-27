using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JeyLapse.Annotations;
using JeyLapse.Session;

namespace JeyLapse.Operations
{
    public class PowerSaverManager : INotifyPropertyChanged
    {
        public PowerSaverProfile CurrentProfile
        {
            get { return _currentProfile; }
            private set
            {
                if (Equals(value, _currentProfile)) return;
                _currentProfile = value;
                OnPropertyChanged();
            }
        }

        private List<PowerSaverProfile> AvailableModes;
        private int _CurrentpsIndex;
        private PowerSaverProfile _currentProfile;

        public PowerSaverManager()
        {
            AvailableModes = new List<PowerSaverProfile>()
            {
                new PowerSaverProfile(){Mode = PowerSaverMode.Off, Description = "Screen Remains on at all times."},
                new PowerSaverProfile(){Mode = PowerSaverMode.Interval, Description = "Screen turns on after every 20 seconds and goes off again."}, 
                new PowerSaverProfile(){Mode = PowerSaverMode.Always, Description = "Screen turns off after 5 seconds of Touch inactivity."}
            };

            _CurrentpsIndex = Int32.Parse(new SessionProfileHelper().GetProfile("PowerSaver").Value);

            CurrentProfile = AvailableModes[_CurrentpsIndex];
        }

        public void CycleProfile()
        {
            CurrentProfile = AvailableModes[(_CurrentpsIndex + 1)%AvailableModes.Count];

            _CurrentpsIndex = (_CurrentpsIndex + 1) % AvailableModes.Count;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
