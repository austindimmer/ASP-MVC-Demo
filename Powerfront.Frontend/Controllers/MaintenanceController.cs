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
        IUnitOfWork _uow;
        CustomerService _customerService;

        public MaintenanceController()
        {
            _uow = new UnitOfWork<CustomersDbContext>();
            _customerService = new CustomerService(_uow);
        }
        // GET: CustomerRecords
        public async Task<ActionResult> Index()
        {
            IEnumerable<AggregateCustomer> customerRecords = _customerService.GetAllCustomers();
            List<AggregateCustomerViewModel> viewModel = new List<AggregateCustomerViewModel>();
            foreach (var customerRecord in customerRecords)
            {
                //var vm = Mapper.Map<AggregateCustomer, AggregateCustomerViewModel>(customerRecord);
                var vm = new AggregateCustomerViewModel();
                vm.CustomerDataRecords = new List<CustomerRecordViewModel>();
                foreach (var record in customerRecord.CustomerDataRecords)
                {
                    var mappedRecord = Mapper.Map<CustomerRecord, CustomerRecordViewModel>(record);
                    vm.CustomerDataRecords.Add(mappedRecord);
                    vm.CustomerId = mappedRecord.CustomerId;
                }
                //vm.CustomerId = Guid.NewGuid().ToString();
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
            if (customerRecord == null)
            {
                return HttpNotFound();
            }
            return View(customerRecord);
        }

        // GET: Maintenance/Create
        public ActionResult Create()
        {
            //ViewBag.PropertyId = new SelectList(db.Properties, "PropertyId", "Name");
            //ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name");
            return View();
        }

        // POST: Maintenance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TypeId,PropertyId,Value,CustomerId,RecordId")] CustomerRecord customerRecord)
        {
            if (ModelState.IsValid)
            {
                //customerRecord.RecordId = Guid.NewGuid();
                //db.CustomerRecords.Add(customerRecord);
                //await db.SaveChangesAsync();
                //return RedirectToAction("Index");
            }

            //ViewBag.PropertyId = new SelectList(db.Properties, "PropertyId", "Name", customerRecord.PropertyId);
            //ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name", customerRecord.TypeId);
            return View(customerRecord);
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
            var serializableCustomerReocrd = Mapper.Map<AggregateCustomer, AggregateCustomerViewModel>(retrievedCustomerRecord);

            var jsonCustomerRecord = AggregateCustomerViewModel.Serialize(serializableCustomerReocrd);

            var jsonToReturn = Json(serializableCustomerReocrd, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            return jsonToReturn;
        }

        [HttpPost]
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