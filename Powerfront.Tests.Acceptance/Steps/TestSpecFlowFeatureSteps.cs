using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Powerfront.Acceptance.Tests.Common;
using System;
using System.Diagnostics;
using System.Threading;
using TechTalk.SpecFlow;
using Powerfront.Acceptance.Tests.Extensions;

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

            //http://stackoverflow.com/questions/6992993/selenium-c-sharp-webdriver-wait-until-element-is-present
            //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            driver.Navigate().GoToUrl("https://localhost:44337/");
            var credentialsInput = driver.FindElementById("cred_userid_inputtext");
            credentialsInput.SendKeys(Common.FeatureContext.TestUserName);
            var credentialsPasswordInput = driver.FindElementById("cred_password_inputtext");
            credentialsPasswordInput.SendKeys(Common.FeatureContext.TestUserPassword);

            // http://stackoverflow.com/questions/35741558/selenium-failing-to-identify-an-actionlink-on-the-aad-login-page/35746326
            //var button = driver.FindElementByClassName("cred_sign_in_button");
            //var button = driver.FindElementById("cred_sign_in_button");

            IWebElement button = driver.FindElement(By.Id("cred_sign_in_button"), 10);




                //WebDriverWait[is] from the OpenQA.Selenium.Support.UI namespace and comes in a separate package called Selenium WebDriver Support Classes on NuGet


                //In Visual Studio this means, that you need to install TWO packages:

                //NuGet package "Selenium.WebDriver" AND ALSO
                //NuGet package "Selenium.Support"

            //WebDriverWait wait = new WebDriverWait(browser, new TimeSpan(0, 5, 0));

            //bool clicked = wait.Until<bool>((elem) =>
            //{
            //    elem.Click(); //Doesn't Work
            //    return true;
            //});
            //driver.FindElement(By.Xpath('//p[contains(text(),"Sign in with a username and password instead")]')).Click();
            Assert.IsNotNull(button);
            //driver.ExecuteScript("$(arguments[0]).click()", button);
            //button.Click();
            button.Click();
            //button.SendKeys(Keys.Enter);


            Thread.Sleep(4000);
            String url = driver.Url;
            Assert.AreEqual(url, "https://localhost:44337/");
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
