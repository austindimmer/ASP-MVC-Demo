using Powerfront.Backend.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Powerfront.Frontend.Tests.Mock
{
    public class MockDataContext
    {
        public List<CustomerRecord> CustomerRecord
        {
            get
            {
                return new List<CustomerRecord>
            {
                new CustomerRecord
                {
                    CustomerId= "1",
                    TypeId = "1",
                    PropertyId = "1",
                    Value= "Hadar",
                    Type = new Backend.EntityFramework.Type { TypeId= "1", Name="Customer" },
                    Property = new Backend.EntityFramework.Property {  PropertyId= "1", Name="Name" },
                    RecordId = Guid.NewGuid()
                },
                new CustomerRecord
                {
                    CustomerId= "1",
                    TypeId = "1",
                    PropertyId = "2",
                    Value= "47",
                    Type = new Backend.EntityFramework.Type { TypeId= "1", Name="Customer" },
                    Property = new Backend.EntityFramework.Property {  PropertyId= "2", Name="Age" },
                    RecordId = Guid.NewGuid()
                },
                new CustomerRecord
                {
                    CustomerId= "2",
                    TypeId = "1",
                    PropertyId = "1",
                    Value= "Greg",
                    Type = new Backend.EntityFramework.Type { TypeId= "1", Name="Customer" },
                    Property = new Backend.EntityFramework.Property {  PropertyId= "1", Name="Name" },
                    RecordId = Guid.NewGuid()
                },
                new CustomerRecord
                {
                    CustomerId= "2",
                    TypeId = "1",
                    PropertyId = "2",
                    Value= "38",
                    Type = new Backend.EntityFramework.Type { TypeId= "1", Name="Customer" },
                    Property = new Backend.EntityFramework.Property {  PropertyId= "2", Name="Age" },
                    RecordId = Guid.NewGuid()
                },
                new CustomerRecord
                {
                    CustomerId= "3",
                    TypeId = "1",
                    PropertyId = "1",
                    Value= "Michael",
                    Type = new Backend.EntityFramework.Type { TypeId= "1", Name="Customer" },
                    Property = new Backend.EntityFramework.Property {  PropertyId= "1", Name="Name" },
                    RecordId = Guid.NewGuid()
                },
                new CustomerRecord
                {
                    CustomerId= "3",
                    TypeId = "1",
                    PropertyId = "2",
                    Value= "47",
                    Type = new Backend.EntityFramework.Type { TypeId= "1", Name="Customer" },
                    Property = new Backend.EntityFramework.Property {  PropertyId= "2", Name="Age" },
                    RecordId = Guid.NewGuid()
                }
            };
            }
        }

        public List<Property> Property
        {
            get
            {
                return new List<Property>
            {
                new Property
                {
                    PropertyId= "1",
                    Name = "Name"
                },
                new Property
                {
                    PropertyId= "2",
                    Name = "Age"
                }
            };
            }
        }

        public List<Powerfront.Backend.EntityFramework.Type> Type
        {
            get
            {
                return new List<Powerfront.Backend.EntityFramework.Type>
            {
                new Powerfront.Backend.EntityFramework.Type
                {
                    TypeId= "1",
                    Name = "Customer"
                }
            };
            }
        }
    }
}