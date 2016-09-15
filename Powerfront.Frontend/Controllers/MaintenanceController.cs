using AutoMapper;
using Powerfront.Backend.EntityFramework;
using Powerfront.Backend.Model;
using Powerfront.Backend.MVC;
using Powerfront.Backend.Repository;
using Powerfront.Backend.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Powerfront.Backend.Model.JsonModelBinder;

namespace Powerfront.Frontend.Controllers
{
    public class MaintenanceController : Controller
    {
        object lockObject;
        IUnitOfWork _uow;
        CustomerService _customerService;
        PropertyService _propertyService;

        public MaintenanceController()
        {
            _uow = new UnitOfWork<PowerfrontDbContext>();
            _customerService = new CustomerService(_uow);
            _propertyService = new PropertyService(_uow);
        }
        // GET: CustomerRecords
        public async Task<ActionResult> Index()
        {
            IEnumerable<AggregateCustomer> customerRecords = _customerService.GetAllCustomers();
            List<AggregateCustomerViewModel> viewModel = new List<AggregateCustomerViewModel>();
            foreach (var customerRecord in customerRecords)
            {
                var vm = new AggregateCustomerViewModel();
                vm.CustomerDataRecords = new List<CustomerRecordViewModel>();
                foreach (var record in customerRecord.CustomerDataRecords)
                {
                    if (record.Property == null)
                    {
                        record.Property = _propertyService.GetPropertyByID(record.PropertyId);
                    }
                    var mappedRecord = Mapper.Map<CustomerRecord, CustomerRecordViewModel>(record);

                    vm.CustomerDataRecords.Add(mappedRecord);
                    vm.CustomerId = mappedRecord.CustomerId;
                }
                viewModel.Add(vm);
            }
            return View(viewModel);
        }

        // GET: Maintenance/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AggregateCustomer customerRecord = _customerService.GetCustomerByID(id);
            EnsureProperties(customerRecord);
            if (customerRecord == null)
            {
                return HttpNotFound();
            }
            return View(customerRecord);
        }

        // GET: Maintenance/Create
        public ActionResult Create()
        {

            return View();

        }

        [HandleUIException]
        public async Task<JsonResult> GetNewlyCreatedCustomerJson()
        {
            AggregateCustomer newlyCreatedCustomer = new AggregateCustomer();
            newlyCreatedCustomer.CustomerDataRecords = new List<CustomerRecord>();

            var newlyCreatedCustomerId = Guid.NewGuid().ToString();
            newlyCreatedCustomer.CustomerId = newlyCreatedCustomerId;
            var currentPropertySet = _propertyService.GetAllProperties();
            foreach (var property in currentPropertySet)
            {
                CustomerRecord newCustomerRecord = new CustomerRecord();
                newCustomerRecord.RecordId = Guid.NewGuid();

                Property newProperty = new Property();
                Backend.EntityFramework.Type newType = new Backend.EntityFramework.Type();
                newCustomerRecord.CustomerId = newlyCreatedCustomerId;
                newCustomerRecord.Property = newProperty;
                newCustomerRecord.Type = newType;
                newCustomerRecord.Property = property;
                newCustomerRecord.PropertyId = property.PropertyId;
                // Only setting this property for convenience in a more complete system/example this would be dealt with in a similar way to properties.
                newCustomerRecord.TypeId = "1";
                newlyCreatedCustomer.CustomerDataRecords.Add(newCustomerRecord);
            }
     

            var serializableCustomerReocrd = Mapper.Map<AggregateCustomer, AggregateCustomerViewModel>(newlyCreatedCustomer);

            var jsonCustomerRecord = AggregateCustomerViewModel.Serialize(serializableCustomerReocrd);

            var jsonToReturn = Json(serializableCustomerReocrd, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            return jsonToReturn;
        }

        [ActionName("CreateNewCustomerWithPostedJson")]
        public JsonResult CreateNewCustomerWithPostedJson([JsonBinder]AggregateCustomerViewModel createdCustomer)
        {
            bool addedNewCustomer = false;
            //var createdCustomerFromDb = _customerService.CreateCustomer(new AggregateCustomer());
            if (ModelState.IsValid)
            {
                var aggregateCustomer = Mapper.Map<AggregateCustomerViewModel, AggregateCustomer>(createdCustomer);

                var dbCreationCutomer = Mapper.Map<AggregateCustomerViewModel, AggregateCustomer>(createdCustomer);

                foreach (var item in dbCreationCutomer.CustomerDataRecords)
                {
                    //These properties must be nulled out to preserve EF model referential integrity
                    item.Property = null;
                    item.Type = null;   
                }

                var newAggregateCustomer = _customerService.CreateCustomer(dbCreationCutomer);

                // Now that we have created a customer object update the properties and save in Db
                var currentProperties = _propertyService.GetAllProperties();
                foreach (var property in currentProperties)
                {
                    foreach (var record in newAggregateCustomer.CustomerDataRecords)
                    {
                        if(record.PropertyId == property.PropertyId)
                        {
                            record.Value = createdCustomer.CustomerDataRecords.Where(c => c.PropertyId == property.PropertyId).Select(r => r.Value).FirstOrDefault();
                        }
                    }
                }
                addedNewCustomer = _customerService.UpdateCustomerRecords(newAggregateCustomer);

            }
            if (addedNewCustomer)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
        }


        // GET: Maintenance/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null || id == String.Empty)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AggregateCustomer retrievedCustomerRecord = _customerService.GetCustomerByID(id);
            retrievedCustomerRecord.CustomerId = id;
            AggregateCustomerViewModel serializableCustomerReocrd = Mapper.Map<AggregateCustomer, AggregateCustomerViewModel>(retrievedCustomerRecord);

            var jsonCustomerRecord = AggregateCustomerViewModel.Serialize(serializableCustomerReocrd);
            
            return View(serializableCustomerReocrd);
        }

        [HandleUIException]
        public async Task<JsonResult> GetJsonByCustomerId(string id)
        {
            if (id == null || id == String.Empty)
            {
                var data = "{Customer Id does not exist}" as object;
                //throw new ValidationException(Json(data, "application/json", Encoding.UTF8););
            }

            AggregateCustomer retrievedCustomerRecord = _customerService.GetCustomerByID(id);
            retrievedCustomerRecord.CustomerId = id;

            EnsureProperties(retrievedCustomerRecord);

            var serializableCustomerReocrd = Mapper.Map<AggregateCustomer, AggregateCustomerViewModel>(retrievedCustomerRecord);

            var jsonCustomerRecord = AggregateCustomerViewModel.Serialize(serializableCustomerReocrd);

            var jsonToReturn = Json(serializableCustomerReocrd, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            return jsonToReturn;
        }

        private void EnsureProperties(AggregateCustomer retrievedCustomerRecord)
        {
            foreach (var item in retrievedCustomerRecord.CustomerDataRecords)
            {
                if (item.Property == null)
                {
                    item.Property = _propertyService.GetPropertyByID(item.PropertyId);
                }
            }
        }

        [HttpPost]
        // TODO: need to implement idea outlined here http://stackoverflow.com/questions/4074199/jquery-ajax-calls-and-the-html-antiforgerytoken
        //[ValidateAntiForgeryToken]
        [ActionName("EditPostedJson")]
        public JsonResult EditPostedJson([JsonBinder]AggregateCustomerViewModel updatedCustomer)
        //public async Task<ActionResult> Edit(AggregateCustomer customerRecord)
        {
            var editedCustomerId = ViewBag.CustomerId;
            bool editedCustomer=false;
            if (ModelState.IsValid)
            {
                AggregateCustomer newRecord = new AggregateCustomer();
                newRecord.CustomerDataRecords = new List<CustomerRecord>();
                var retrievedCustomer = _customerService.GetCustomerByID(updatedCustomer.CustomerId);
                EnsureProperties(retrievedCustomer);
                for (int i = 0; i < updatedCustomer.CustomerDataRecords.Count; i++)
                {
                    var currentUpdatedProperty = updatedCustomer.CustomerDataRecords[i].Property.Name;
                    for (int j = 0; j < retrievedCustomer.CustomerDataRecords.Count; j++)
                    {
                        var currentRetreivedProperty = retrievedCustomer.CustomerDataRecords[j].Property.Name;
                        if (currentRetreivedProperty == currentUpdatedProperty)
                        {
                            retrievedCustomer.CustomerDataRecords[j].Value = updatedCustomer.CustomerDataRecords[i].Value;
                        }
                    }

                    var valueToUpdateOnRetrievedCustomer = retrievedCustomer.CustomerDataRecords.Where(x => x.Property.Name == currentUpdatedProperty).Select(p => p.Value).FirstOrDefault();
                    valueToUpdateOnRetrievedCustomer = updatedCustomer.CustomerDataRecords[i].Value;
                }

                editedCustomer = _customerService.UpdateCustomerRecords(retrievedCustomer);

            }
            if (editedCustomer)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Maintenance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost ]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public ActionResult EditPosted([JsonBinder]AggregateCustomerViewModel modelString)
            //public async Task<ActionResult> Edit(AggregateCustomer customerRecord)
        {
            var editedCustomerId = ViewBag.CustomerId;
            if (ModelState.IsValid)
            {
                AggregateCustomerViewModel customerRecord = modelString;
                AggregateCustomer newRecord = new AggregateCustomer();
                newRecord.CustomerDataRecords = new List<CustomerRecord>();
                var retrievedCustomer = _customerService.GetCustomerByID(customerRecord.CustomerId);
                EnsureProperties(retrievedCustomer);
                foreach (var updatedRecord in customerRecord.CustomerDataRecords)
                {
                    var mappedRecord = Mapper.Map<CustomerRecordViewModel, CustomerRecord>(updatedRecord);
                }
                
                var editedCustomer = 
                    _customerService.UpdateCustomerRecords(retrievedCustomer);
                return RedirectToAction("Index");

            }
            return View(modelString);
        }

        // GET: Maintenance/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AggregateCustomer customerRecord = _customerService.GetCustomerByID(id);
            if (customerRecord == null)
            {
                return HttpNotFound();
            }
            return View(customerRecord);
        }

        // POST: Maintenance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AggregateCustomer customerRecord = _customerService.GetCustomerByID(id);
            if (customerRecord == null)
            {
                return HttpNotFound();
            }
            _customerService.DeleteCustomerByID(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _uow.Dispose();
            }
            base.Dispose(disposing);
        }

        private T Deserialise<T>(string json)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serialiser = new DataContractJsonSerializer(typeof(T));
                return (T)serialiser.ReadObject(ms);
            }
        }
    }
}