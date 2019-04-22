using System.Windows;

namespace GoogleAnalytics
{
    class ScreenInfo
    {
        public static string FullResolution => $"{SystemParameters.PrimaryScreenWidth}x{SystemParameters.PrimaryScreenHeight}";
        public static double Height => SystemParameters.PrimaryScreenHeight;
        public static double Width => SystemParameters.PrimaryScreenWidth;
    }
}
