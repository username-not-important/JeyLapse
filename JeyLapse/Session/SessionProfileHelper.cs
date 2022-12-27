using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JeyLapse.Session
{
    public class SessionProfileHelper
    {
        public Session SavedSession
        {
            get
            {
                int interval = Int32.Parse(GetProfile("Interval").Value);
                int duration = Int32.Parse(GetProfile("Duration").Value);
                int flash = Int32.Parse(GetProfile("Flash").Value);
                int power = Int32.Parse(GetProfile("PowerSaver").Value);
                bool wide = GetProfile("WideScreen").Value != "0";
                bool lck = GetProfile("UnderLock").Value != "0";
                string key = GetProfile("Key").Value;

                string resolution = GetProfile("Resolution").Value;
                int iX = resolution.IndexOf('x');

                Size res = new Size(Double.Parse(resolution.Substring(0,iX)), Double.Parse(resolution.Substring(iX+1)));

                FlashMode fmode = flash == -1 ? FlashMode.Off : (flash == 1 ? FlashMode.On : FlashMode.Auto);

                PowerSaverMode pmode = power == 0 ? PowerSaverMode.Off : (power == 1 ? PowerSaverMode.Interval : PowerSaverMode.Always);

                Session s = new Session(TimeSpan.FromSeconds(interval), TimeSpan.FromMinutes(duration) , fmode, res, pmode, wide, lck,key);

                return s;
            }
            set
            {
                SessionProfileDataContext db = App.DB;

                Session s = value;
                if (s == null) return;

                GetProfile("Interval").Value = s.Interval.TotalSeconds.ToString();
                GetProfile("Duration").Value = s.Duration.TotalMinutes.ToString();
                GetProfile("Flash").Value = s.Flash == FlashMode.Auto ? "0" : (s.Flash == FlashMode.On ? "1" : "-1");
                GetProfile("Resolution").Value = s.Resolution.Width + "x" + s.Resolution.Height;
                GetProfile("PowerSaver").Value = s.PowerSaver == PowerSaverMode.Off ? "0" : (s.PowerSaver == PowerSaverMode.Interval ? "1" : "2");
                GetProfile("WideScreen").Value = s.WideScreen ? "1" : "0";
                GetProfile("UnderLock").Value = s.UnderLock ? "1" : "0";
                GetProfile("Key").Value = s.Key;

                db.SubmitChanges();
            }
        }

        public SessionProfile GetProfile(string Key)
        {
            SessionProfileDataContext db = App.DB;

            var rows = from SessionProfile ps in db.Items
                       where ps.Key == Key
                       select ps;

            if (rows.Count() == 0)
                return null;

            return rows.First();
        }
    }
}
