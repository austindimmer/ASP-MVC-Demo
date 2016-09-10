using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Powerfront.Acceptance.Tests.Common;
using Powerfront.Acceptance.Tests.Selenium;
using System;
using System.Diagnostics;
using System.Threading;
using TechTalk.SpecFlow;

namespace Powerfront.Acceptance.Tests.Steps
{
    [Binding]
    public class TestSpecFlowFeatureSteps : StepBase
    {
        [Given(@"I have navigated to the maintenance page")]
        public void GivenIHaveNavigatedToTheMaintenancePage()
        {
            //ScenarioContext.Current.Pending();
            var driver = Common.FeatureContext.WebDriver;

            driver.Navigate().GoToUrl(Common.FeatureContext.TestApplicationBaseUrl);
            var credentialsInput = driver.FindElementById("cred_userid_inputtext");
            credentialsInput.SendKeys(Common.FeatureContext.TestUserName);
            var credentialsPasswordInput = driver.FindElementById("cred_password_inputtext");
            credentialsPasswordInput.SendKeys(Common.FeatureContext.TestUserPassword);

            // http://stackoverflow.com/questions/35741558/selenium-failing-to-identify-an-actionlink-on-the-aad-login-page/35746326
            //var button = driver.FindElementByClassName("cred_sign_in_button");
            //var button = driver.FindElementById("cred_sign_in_button");

            IWebElement button = driver.FindElement(By.Id("cred_sign_in_button"), 10);
            driver.TakeScreenshot(Common.FeatureContext.TestScreenshotsBasePath + "LoginScreen.png", Common.FeatureContext.TestImageFromat);

            Assert.IsNotNull(button);
            //driver.ExecuteScript("$(arguments[0]).click()", button);
            //button.Click();
            button.Click();
            //button.SendKeys(Keys.Enter);


            Thread.Sleep(4000);
            String url = driver.Url;
            driver.TakeScreenshot(Common.FeatureContext.TestScreenshotsBasePath + "WelcomeScreen.png", Common.FeatureContext.TestImageFromat);
            Assert.AreEqual(url, Common.FeatureContext.TestApplicationBaseUrl);
            driver.Quit();
        }
        
        [Given(@"I have selected to see a list of customers and ages")]
        public void GivenIHaveSelectedToSeeAListOfCustomersAndAges()
        {
            //ScenarioContext.Current.Pending();
        }
        
        [When(@"I click list customers")]
        public void WhenIClickListCustomers()
        {
            //ScenarioContext.Current.Pending();
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
    }
}
