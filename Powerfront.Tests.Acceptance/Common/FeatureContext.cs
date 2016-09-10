using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using TechTalk.SpecFlow;

namespace Powerfront.Acceptance.Tests.Common
{
    [TestClass]
    public sealed class FeatureContext
    {

        public static RemoteWebDriver WebDriver { get; private set; }

        public static string TestUserName { get; private set; }
        public static string TestUserPassword { get; private set; }
        public static string TestApplicationBaseUrl { get; private set; }
        public static string TestScreenshotsBasePath { get; private set; }

        public static ImageFormat TestImageFromat {get ; private set;}


        public static string ApiBaseUrl
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("_apiBaseUrl"))
                {
                    ScenarioContext.Current["_apiBaseUrl"] = ConfigurationManager.AppSettings["api.url"];
                }
                return ScenarioContext.Current.Get<string>("_apiBaseUrl");
            }
            set { ScenarioContext.Current["_apiBaseUrl"] = value; }
        }

        [AssemblyInitialize]
        public static void Start(TestContext context)
        {
            FirefoxProfileManager profileManager = new FirefoxProfileManager();
            FirefoxProfile profile = profileManager.GetProfile("profileToolsQA");
            WebDriver = new FirefoxDriver(profile);
            TestUserName = "powerfront@effective-computing.com";
            TestUserPassword = "ReallyToughPass29!~8763";
            TestApplicationBaseUrl = @"https://localhost:44337/";
            TestImageFromat = ImageFormat.Png;
            TestScreenshotsBasePath = @"C:\Temp\Powerfront\";

            if (!Directory.Exists(TestScreenshotsBasePath)) {
                Directory.CreateDirectory(TestScreenshotsBasePath);
            }

        }

        [AssemblyCleanup]
        public static void Stop()
        {
            WebDriver.Close();
            WebDriver.Dispose();
        }
    }
}
