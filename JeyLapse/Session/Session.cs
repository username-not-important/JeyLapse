using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Devices;

namespace JeyLapse.Session
{
    public enum FlashMode
    {
        On,Off,Auto
    }

    public enum PowerSaverMode
    {
        Off, Always, Interval
    }

    public class Session
    {
        public TimeSpan Interval { get; private set; }
        public TimeSpan Duration { get; private set; }
        public FlashMode Flash { get; private set; }
        public Size Resolution { get; private set; }
        public PowerSaverMode PowerSaver { get; private set; }
        public bool WideScreen { get; private set; }
        public bool UnderLock { get; private set; }
        public string Key { get; private set; }

        public Session(TimeSpan interval, TimeSpan duration, FlashMode flash, Size resolution, PowerSaverMode powerSaver,bool wideScreen,bool underLock, string key)
        {
            Interval = interval;
            Duration = duration;
            Flash = flash;
            Resolution = resolution;
            PowerSaver = powerSaver;
            WideScreen = wideScreen;
            UnderLock = underLock;
            Key = key;
        }


    }
}
