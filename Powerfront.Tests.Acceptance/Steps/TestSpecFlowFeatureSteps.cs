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
