using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web;

namespace Microsoft.PowerToys.Run.Plugin.Google
{
    public static class GoogleSearch
    {
        private const string GooglePrefix = "https://www.google.com/search?q=";
        
        //TODO: Detect default browser and offer incognito/private if supported
        public static void OpenInDefaultBrowser (string searchTerm)
        {
            LaunchBrowser(GetGoogleSearchUrl(searchTerm));
        }
        
        // Placeholder until browser detection is implemented
        public static void OpenInEdge(string searchTerm, bool inPrivateSession)
        {
            var arguments = new List<string>
            {
                "msedge",
                GetGoogleSearchUrl(searchTerm)
            };

            if (inPrivateSession)
                arguments.Add("-inPrivate");
            
            LaunchBrowser(arguments.ToArray());
        }
        
        private static string GetGoogleSearchUrl(string searchTerm) =>
            $"{GooglePrefix}{HttpUtility.UrlEncode(searchTerm)}";

        private static void LaunchBrowser( params string[] args)
        {
            var builder = new StringBuilder("/c start");
            
            foreach (var arg in args)
            {
                builder.Append(" ");
                builder.Append(arg);
            }
            
            var info = new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = builder.ToString(),
                CreateNoWindow = true
            };

            Process.Start(info);
        }
    }
}