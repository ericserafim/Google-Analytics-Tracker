using System;
using System.Net;
using System.Collections;
using System.Web;

namespace GoogleAnalytics
{
    public class GoogleAnalyticsTracker
    {
        private enum HitType
        {            
            @event,            
            @exception,
            @screenview
        }

        private string API_URL = "https://www.google-analytics.com/collect";
        private string API_VERSION = "1";

        public string TrackingId { get; set; }
        public string AppName { get; set; }
        public string AppVersion { get; set; }
        public string ClientId { get; set; }
        public string UserId { get; set; }

        //public string Erro { get; private set; } = "";

        public void TrackEvent(string category, string action, string label, int value = 0)
        {
            Hashtable ht = new Hashtable();            
            ht.Add("ec", category);// Event Category. Required.
            ht.Add("ea", action);  // Event Action. Required.
            ht.Add("el", label);   // Event label.
            ht.Add("ev", value);   // Event value.

            SendTrack(HitType.@event, ht);
        }

        public void TrackScreenView(string name, string caption)
        {
            Hashtable ht = new Hashtable();
            ht.Add("cd", $"{name} ({caption})");    // Event Category. Required.
            //ht.Add("ea", "Open");                   // Event Action. Required.

            SendTrack(HitType.@screenview, ht);
        }

        public void TrackException(string description, bool fatal)
        {
            Hashtable ht = new Hashtable();            
            ht.Add("dh", description);            // Exception description.         Required.
            ht.Add("dp", fatal ? "1" : "0");      // Exception is fatal?            Required.

            SendTrack(HitType.@exception, ht);
        }

        private void SendTrack(HitType type, Hashtable hashtable)
        {
            Hashtable values = new Hashtable();
            values.Add("v", API_VERSION);
            values.Add("tid", TrackingId);
            values.Add("cid", ClientId);
            values.Add("uid", UserId);
            values.Add("ds", AppName);         // Data Source
            values.Add("an", AppName);         // Application name
            values.Add("av", AppVersion);      // Application name
            values.Add("t", type.ToString());  //Event type
            values.Add("sr", ScreenInfo.FullResolution);  //Screen resolution Example: sr=800x600
            values.Add("ua", OSVersionInfo.FullName());   //User Agent Override Example: Opera/9.80 (Windows NT 6.0) Presto/2.12.388 Version/12.14

            foreach (DictionaryEntry item in hashtable)
            {
                values.Add(item.Key, item.Value);
            }

            string data = "";
            foreach (var key in values.Keys)
            {
                if (data != "") data += "&";
                if (values[key] != null) data += key.ToString() + "=" + HttpUtility.UrlEncode(values[key].ToString());
            }

            RequestApi(data);
        }

        private async void RequestApi(string data)
        {
            using (var client = new WebClient())
            {
                try
                {                    
                    string result = await client.UploadStringTaskAsync(API_URL, "POST", data);                    
                }
                catch
                {
                    //Erro = e.Message;
                }

            }
        }

    }
}
