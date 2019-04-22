using System.Collections.Generic;
using System.Management;

namespace GoogleAnalytics
{
    public class OSVersionInfo
    {
        public static string FullName()
        {
            KeyValuePair<string, string> kvpOSSpecs = new KeyValuePair<string, string>();
            
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption, Version FROM Win32_OperatingSystem");

                foreach (var os in searcher.Get())
                {
                    var version = os["Version"].ToString();
                    var productName = os["Caption"].ToString();
                    kvpOSSpecs = new KeyValuePair<string, string>(productName, version);
                }
            }
            catch {
                return "Unknown";
            }

            return kvpOSSpecs.ToString();
        }


    }
}
