using System.Net;
using SaveSettings;

namespace PocketPC
{
    public class Coneccion
    {
        public static IPEndPoint IP
        {
            get
            {
                IPAddress ip;
                if (Settings.UsarDNS)
                {
                    IPHostEntry IPHost = Dns.GetHostEntry(Settings.DNS);
                    IPAddress[] ipAddress = IPHost.AddressList;
                    ip = ipAddress[0];
                }
                else
                {
                    ip = IPAddress.Parse(Settings.IP);
                }

                IPEndPoint ipEnd = new IPEndPoint(ip, Settings.Puerto);
                return ipEnd;
            }

        }
    }
}
