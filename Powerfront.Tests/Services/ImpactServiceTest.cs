using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Powerfront.Backend.Repository;
using Powerfront.Backend.Services;
using Powerfront.Frontend.Tests.Mock;
using Powerfront.Backend.EntityFramework;
using Powerfront.Backend.Model;
using Powerfront.Backend.Impact.Services;
using Powerfront.Backend.Impact.EntityFramework;

namespace Powerfront.Frontend.Tests.Services
{
    [TestClass]
    public class ImpactServiceTest
    {
        IUnitOfWork _uow;
        ImpactService _ImpactService;

        [TestInitialize]
        public void SetUp()
        {
            _uow = new MockUnitOfWork<MockImpactDataContext>();
            _ImpactService = new ImpactService(_uow);
        }

        [TestCleanup]
        public void TearDown()
        {
            // Clean resources
        }

        [TestMethod]
        public void CanGetAggregatedImpactRecords ()
        {
            var impacts = _ImpactService.GetAllImpacts();
            var numberOfImpacts = impacts.Count();
            Assert.AreEqual(3, numberOfImpacts);
        }

        [TestMethod]
        public void CanUpdateImpactRecord()
        {
            try
            {
                var impactId = Guid.Parse("8a34c4f4-fb0f-4207-ac5c-ba9ec1bb71c3");
                var impact = _ImpactService.GetImpactByID(impactId);
                Assert.AreEqual(impact.ImpactId, impactId);
                Assert.AreEqual(impact.ImpactName, "Impact 1");

                var updatedName = "Updated Impact Name";
                impact.ImpactName = updatedName;
                _ImpactService.UpdateImpactRecord(impact);
                var retrievedImpact = _ImpactService.GetImpactByID(impactId);
                Assert.AreEqual(impact.ImpactId, impactId);
                Assert.AreEqual(impact.ImpactName, updatedName);


            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        [TestMethod]
        public void CanCreateNewImpact()
        {
            try
            {
                var impactName = "Impact 4";
                var createdImpactId = Guid.Parse("8a34c4f4-fb0f-4207-ac5c-ba9ec1bb71d7");
                var startDate = new DateTime(2017, 01, 01);
                var finishDate = new DateTime(2018, 01, 01);
                var note = "The program will improve sport performance for all young men in the Argyll area.";

                Impact expected = new Impact
                {
                    ImpactName = impactName,
                    StartDate = startDate,
                    FinishDate = finishDate,
                    ImpactId = createdImpactId,
                    Notes = note,
                };

                var impacts = _ImpactService.GetAllImpacts();
                var numberOfImpacts = impacts.Count();
                Assert.AreEqual(3, numberOfImpacts);

                _ImpactService.CreateImpact(expected);
                var retreivedImpacts = _ImpactService.GetAllImpacts();
                numberOfImpacts = retreivedImpacts.Count();
                Assert.AreEqual(4, numberOfImpacts);
                Impact actual = _ImpactService.GetImpactByID(createdImpactId);

                Assert.AreSame(expected, actual);

            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }

        //[TestMethod]
        //[ExpectedException(typeof(NullReferenceException))]
        //public void CreatingNewImpactWithSameIdThrows()
        //{
        //    Impact existing = new Impact
        //    {
        //        ImpactName = "Impact 2",
        //        StartDate = new DateTime(2017, 01, 01),
        //        FinishDate = new DateTime(2018, 01, 01),
        //        ImpactId = Guid.Parse("8a34c4f4-fb0f-4207-ac5c-ba9ec1bb71c4"),
        //        Notes = "The program will improve sport performance for all disabled people in the Aberdeenshire area.",
        //        BeneficiaryGroup = new BeneficiaryGroup
        //        {
        //            BeneficiaryGroupDescription = "People With Disabilities",
        //            BeneficiaryGroupId = Guid.Parse("510d4419-fdac-4cf7-a529-fa1bc2817c8d"),
        //        }
        //    };
        //    _ImpactService.CreateImpact(existing);
        //}

        [TestMethod]
        public void CanGetImpactByID()
        {
            Impact expected = new Impact
            {
                ImpactName = "Impact 2",
                StartDate = new DateTime(2017, 01, 01),
                FinishDate = new DateTime(2018, 01, 01),
                ImpactId = Guid.Parse("8a34c4f4-fb0f-4207-ac5c-ba9ec1bb71c4"),
                Notes = "The program will improve sport performance for all disabled people in the Aberdeenshire area.",
                BeneficiaryGroup = new BeneficiaryGroup
                {
                    BeneficiaryGroupDescription = "People With Disabilities",
                    BeneficiaryGroupId = Guid.Parse("510d4419-fdac-4cf7-a529-fa1bc2817c8d"),
                }
            };

            var actual = _ImpactService.GetImpactByID(Guid.Parse("8a34c4f4-fb0f-4207-ac5c-ba9ec1bb71c4"));
            Assert.AreEqual(expected.ImpactId, actual.ImpactId);
            Assert.AreEqual(expected.ImpactName, actual.ImpactName);
        }

        [TestMethod]
        public void CanDeleteImpactByID()
        {
            try
            {
                var impactId = Guid.Parse("8a34c4f4-fb0f-4207-ac5c-ba9ec1bb71c3");

                _ImpactService.DeleteImpactByID(impactId);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}