using OpenQA.Selenium.DevTools.V125.Audits;
using System.Collections.Generic;

namespace Task_Framework.Config
{
    public class AppSettings
    {
        public string Environment { get; set; }
        public Dictionary<string, string> Environments { get; set; }
        public BrowserSettings Browser { get; set; }
        public Credentials Credentials { get; set; }
    }

    public class BrowserSettings
    {
        public string Name { get; set; }
        public bool Headless { get; set; }
        public int ImplicitWait { get; set; }
        public int PageLoadTimeout { get; set; }
        public int ScriptTimeout { get; set; }
        public bool StartMaximized { get; set; }
        public ResolutionSettings Resolution { get; set; }
    }

    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ResolutionSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
