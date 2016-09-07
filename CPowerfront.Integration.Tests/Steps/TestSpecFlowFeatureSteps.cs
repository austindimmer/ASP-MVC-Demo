using Microsoft.VisualStudio.TestTools.UnitTesting;
using Powerfront.Acceptance.Tests.Common;
using System;
using System.Diagnostics;
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
            driver.Navigate().GoToUrl("https://localhost:44337/");
            var availability = driver.FindElementById("availability");
            //Assert.AreEqual(expectedMessage, availability.Text);
        }
        
        [Given(@"I have selected to see a list of customers and ages")]
        public void GivenIHaveSelectedToSeeAListOfCustomersAndAges()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I click list customers")]
        public void WhenIClickListCustomers()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the resulting page should display a list of cutomers and their ages")]
        public void ThenTheResultingPageShouldDisplayAListOfCutomersAndTheirAges()
        {
            ScenarioContext.Current.Pending();
        }

        [BeforeScenario("resetDatabase")]
        //[AfterScenario("resetDatabase")]
        public void ResetDatabase()
        {
            var p = Process.Start("sqlcmd", @"-S AustinM14X\MSSQLSERVER -d Powerfront.SpecFlow.Database -i PowerfrontCreateDbAndInsertData.sql");
            p.WaitForExit();
        }
    }
}
