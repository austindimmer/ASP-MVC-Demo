using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Powerfront.Acceptance.Tests.Selenium;
using System;
using System.Diagnostics;
using System.IO;
using TechTalk.SpecFlow;

namespace Powerfront.Acceptance.Tests.Steps
{
    [Binding]
    public class DisplayAListOfCutomersSteps
    {
        static RemoteWebDriver driver;
        static string ScenarioSnapShotsFolderPath { get; set; }

        [Given(@"I have navigated to the application root")]
        public void GivenIHaveNavigatedToTheApplicationRoot()
        {
            driver = Common.FeatureContext.WebDriver;
            driver.Navigate().GoToUrl(Common.FeatureContext.TestApplicationBaseUrl);

            driver.TakeScreenshot(ScenarioSnapShotsFolderPath + "1 - HomePage.png", Common.FeatureContext.TestImageFromat);
            String url = driver.Url;
            Assert.AreEqual(url, Common.FeatureContext.TestApplicationBaseUrl);
;
        }
        
        [When(@"I click the maintenance button")]
        public void WhenIClickTheMaintenanceButton()
        {
            IWebElement button = driver.FindElement(By.Id("MaintenanceButton"), 10);
            Assert.IsNotNull(button);

            button.Click();
            String url = driver.Url;
            Assert.AreEqual(url, Common.FeatureContext.TestApplicationBaseUrl + "Maintenance");
            driver.TakeScreenshot(Common.FeatureContext.TestScreenshotsBasePath + "2 - MaintenanceScreen.png", Common.FeatureContext.TestImageFromat);
        }

        [Then(@"the resulting page should display a list of cutomers and their ages")]
        public void ThenTheResultingPageShouldDisplayAListOfCutomersAndTheirAges()
        {
            //ScenarioContext.Current.Pending();
        }

        [BeforeScenario("resetDatabase")]
        //[AfterScenario("resetDatabase")]
        public void ResetDatabase()
        {
            var p = Process.Start("sqlcmd", @"-S AustinM14X\MSSQLSERVER -d Powerfront -i PowerfrontCreateDbTablesAndInsertTestData.sql");
            p.WaitForExit();
        }

        [BeforeScenario("createScenarioSnapshotsFolder")]
        public void CreateScenarioSnapshotsFolder()
        {
            if (!Directory.Exists(Common.FeatureContext.TestScreenshotsBasePath))
            {
                Directory.CreateDirectory(Common.FeatureContext.TestScreenshotsBasePath);
            }
            ScenarioSnapShotsFolderPath = Common.FeatureContext.TestScreenshotsBasePath + "DisplayAListOfCutomersFeatureSteps";

            if (!Directory.Exists(ScenarioSnapShotsFolderPath))
            {
                Directory.CreateDirectory(ScenarioSnapShotsFolderPath);
            }
        }
    }
}
