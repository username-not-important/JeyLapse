using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Phone.Net.NetworkInformation;

namespace JeyLapse.Operations
{
    public class NetworkNotifier
    {
        public static bool ManageNavigation()
        {
            List<string> networkList = new List<string>();

            if (DeviceNetworkInformation.IsCellularDataEnabled)
                networkList.Add("Cellular Data");

            if (DeviceNetworkInformation.IsWiFiEnabled)
                networkList.Add("Wifi Adapter");

            if (networkList.Count == 0)
                return true;

            string networkStatus = networkList[0];
            for (int i = 1; i < networkList.Count; i++)
                networkStatus += " and " + networkList[i];

            var prompt = MessageBox.Show(
                "Your " + networkStatus +
                " is On. This increases battery usage a lot.\nDo you want to turn them Off now?", "Network Adapters",
                MessageBoxButton.OKCancel);

            if (prompt == MessageBoxResult.Cancel)
                return true;
            else
                return false;
        }
    }
}
