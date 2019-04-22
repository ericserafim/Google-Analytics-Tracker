using System;
using System.Runtime.InteropServices;
using net.r_eg.DllExport;

namespace GoogleAnalytics
{
    public class GoogleAnalytics
    {
        private static GoogleAnalyticsTracker tracker;

        [DllExport(CallingConvention.StdCall)]
        public static void Start(string trackingId, string appName, string appVersion, string clientId, string userId)
        {
            if (tracker == null)
            {
                tracker = new GoogleAnalyticsTracker();
            }

            tracker.TrackingId = trackingId;
            tracker.AppName = appName;
            tracker.AppVersion = appVersion;
            tracker.ClientId = clientId;
            tracker.UserId = userId;
        }

        [DllExport(CallingConvention.StdCall)]
        public static void Stop()
        {
            tracker = null;
        }

        [DllExport(CallingConvention.StdCall)]
        public static void TrackEvent(string category, string action, string label, int value)
        {
            ValidarTracker();

            //if (Internet.HasConnection())
            tracker.TrackEvent(category, action, label, value);
        }

        [DllExport(CallingConvention.StdCall)]
        public static void TrackScreenView(string name, string caption)
        {
            ValidarTracker();

            //if (Internet.HasConnection())
            tracker.TrackScreenView(name, caption);
        }

        [DllExport(CallingConvention.StdCall)]
        public static void TrackException(string description, bool fatal)
        {
            ValidarTracker();

            //if (Internet.HasConnection())
            tracker.TrackException(description, fatal);
        }

        private static void ValidarTracker()
        {
            if (tracker == null)
                throw new Exception("Você precisa iniciar o uso chamando o método 'Start'");
        }
    }
}
