using Powerfront.Backend.EntityFramework;
using Powerfront.Backend.Impact.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Powerfront.Frontend.Tests.Mock
{
    public class MockImpactDataContext
    {
        public List<Impact> Impact
        {
            get
            {
                return new List<Impact>
            {
                new Impact
                {
                     ImpactName = "Impact 1",
                     StartDate= new DateTime(2017, 01, 01),
                     FinishDate= new DateTime(2018, 01, 01),
                     ImpactId= Guid.Parse("8a34c4f4-fb0f-4207-ac5c-ba9ec1bb71c3"),
                     Notes= "The program will improve sport performance for all young men in the Argyll area.",
                },

                new Impact
                {
                     ImpactName = "Impact 2",
                     StartDate= new DateTime(2017, 01, 01),
                     FinishDate= new DateTime(2018, 01, 01),
                     ImpactId= Guid.Parse("8a34c4f4-fb0f-4207-ac5c-ba9ec1bb71c4"),
                     Notes= "The program will improve sport performance for all disabled people in the Aberdeenshire area.",
                },
                                new Impact
                {
                     ImpactName = "Impact 3",
                     StartDate= new DateTime(2017, 01, 01),
                     FinishDate= new DateTime(2018, 01, 01),
                     ImpactId= Guid.Parse("8a34c4f4-fb0f-4207-ac5c-ba9ec1bb71c6"),
                     Notes= "The program has not yet been assigned to a Beneficiary Group.",

                }
            };
            }
        }

        public List<BeneficiaryGroup> BeneficiaryGroup
        {
            get
            {
                return new List<BeneficiaryGroup>
            {
                new BeneficiaryGroup
                {
                     BeneficiaryGroupDescription= "Male",
                     BeneficiaryGroupId = Guid.Parse("510d4419-fdac-4cf7-a529-fa1bc2817c8b"),
                },
                new BeneficiaryGroup
                {
                     BeneficiaryGroupDescription= "Female",
                     BeneficiaryGroupId = Guid.Parse("510d4419-fdac-4cf7-a529-fa1bc2817c8d"),
                }
            };
            }
        }


    }
}