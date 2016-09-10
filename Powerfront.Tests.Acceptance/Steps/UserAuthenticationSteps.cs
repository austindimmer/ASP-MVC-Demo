using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Powerfront.Acceptance.Tests.Selenium;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace Powerfront.Acceptance.Tests.Steps
{
    [Binding]
    public class UserAuthenticationSteps
    {
        static RemoteWebDriver driver;


        [Given(@"I need to access information in the eCommerce system")]
        public void GivenINeedToAccessInformationInTheECommerceSystem()
        {
            driver = Common.FeatureContext.WebDriver;
        }
        
        [Given(@"I am not authenticated")]
        public void GivenIAmNotAuthenticated()
        {
            // the user should not be authenticate during a new web driver session
        }
        
        [When(@"I navigate to the application url")]
        public void WhenINavigateToTheApplicationUrl()
        {

            driver.Navigate().GoToUrl(Common.FeatureContext.TestApplicationBaseUrl);
         
        }
        
        [Then(@"I am asked for credentials")]
        public void ThenIAmAskedForCredentials()
        {
            var credentialsInput = driver.FindElementById("cred_userid_inputtext");
            credentialsInput.SendKeys(Common.FeatureContext.TestUserName);
            var credentialsPasswordInput = driver.FindElementById("cred_password_inputtext");
            credentialsPasswordInput.SendKeys(Common.FeatureContext.TestUserPassword);

            IWebElement button = driver.FindElement(By.Id("cred_sign_in_button"), 10);
            driver.TakeScreenshot(Common.FeatureContext.TestScreenshotsBasePath + "LoginScreen.png", Common.FeatureContext.TestImageFromat);

            Assert.IsNotNull(button);

            button.Click();

            Thread.Sleep(4000);

        }
        
        [Then(@"I after I have authenticated I am directed to the application url that was originally requested")]
        public void ThenIAfterIHaveAuthenticatedIAmDirectedToTheApplicationUrlThatWasOriginallyRequested()
        {
            String url = driver.Url;
            driver.TakeScreenshot(Common.FeatureContext.TestScreenshotsBasePath + "WelcomeScreen.png", Common.FeatureContext.TestImageFromat);
            Assert.AreEqual(url, Common.FeatureContext.TestApplicationBaseUrl);
            driver.Quit();
        }
    }
}
