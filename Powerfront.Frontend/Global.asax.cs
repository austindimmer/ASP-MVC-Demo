using AutoMapper;
using Powerfront.Backend.EntityFramework;
using Powerfront.Backend.Impact.EntityFramework;
using Powerfront.Backend.Impact.Model;
using Powerfront.Backend.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Powerfront.Frontend
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            //PowerfrontEntities pfe = new PowerfrontEntities();
            //var customers = pfe.CustomerRecords.GroupBy(x => x.CustomerId).ToList();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(cfg => {
                cfg.CreateMap<AggregateCustomer, AggregateCustomerViewModel>();
                cfg.CreateMap<AggregateCustomerViewModel, AggregateCustomer>();
                cfg.CreateMap<CustomerRecord, CustomerRecordViewModel>();
                cfg.CreateMap<CustomerRecordViewModel, CustomerRecord>();
                cfg.CreateMap<Backend.EntityFramework.Type, TypeViewModel>();
                cfg.CreateMap<TypeViewModel, Backend.EntityFramework.Type>();
                cfg.CreateMap<Property, PropertyViewModel>();
                cfg.CreateMap<PropertyViewModel, Property>();
                cfg.CreateMap<Impact, ImpactViewModel>();
                cfg.CreateMap<ImpactViewModel, Impact>();
            });

            //ModelBinders.Binders.DefaultBinder = new DefaultDictionaryBinder();
            //ModelBinders.Binders.DefaultBinder = new JsonModelBinder();

            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());


        }
    }
}
