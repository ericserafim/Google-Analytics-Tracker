using System.Net.NetworkInformation;

namespace GoogleAnalytics
{
    public class Internet
    {
        public static bool HasConnection()
        {
            try
            {
                Ping myPing = new Ping();
                string host = "8.8.8.8";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);

                return (reply.Status == IPStatus.Success);
            }
            catch {
                return false;
            }
        }
    }
}
