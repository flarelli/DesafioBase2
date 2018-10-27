using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBase2_Flavia.Utilitarios
{
    public class DriverFactory
    {
        public static IWebDriver INSTANCE { set; get; } = null;

        public static void InitializeDriver(string browser)
        {
            if (INSTANCE == null)
            {
                if (browser.Equals("Chrome") || browser.Equals("Google Chrome") || browser.Equals("chrome") || browser.Equals("google chrome") || browser.Equals("CHROME"))
                {
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddUserProfilePreference("e.default_directory", AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "Downloads"));
                    chromeOptions.AddUserProfilePreference("disable-popup-blocking", "true");

                    INSTANCE = new RemoteWebDriver(new Uri(ConfigurationManager.AppSettings["RemoteUrl"]), chromeOptions.ToCapabilities());
                    //INSTANCE = new ChromeDriver(chromeOptions);
                    INSTANCE.Manage().Window.Maximize();
                }
            }
        }
        public static void CloseDriver()
        {
            if (INSTANCE != null)
            {
                INSTANCE.Quit();
                INSTANCE = null;
            }
        }
    }
}
