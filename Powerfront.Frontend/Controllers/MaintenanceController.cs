using Powerfront.Backend.EntityFramework;
using Powerfront.Backend.Model;
using Powerfront.Backend.Repository;
using Powerfront.Backend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
            return View(customerRecords);
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
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRecord customerRecord = new CustomerRecord();

            //CustomerRecord customerRecord = await db.CustomerRecords.FindAsync(id);
            //if (customerRecord == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.PropertyId = new SelectList(db.Properties, "PropertyId", "Name", customerRecord.PropertyId);
            //ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name", customerRecord.TypeId);
            return View(customerRecord);
        }

        // POST: Maintenance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TypeId,PropertyId,Value,CustomerId,RecordId")] CustomerRecord customerRecord)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(customerRecord).State = EntityState.Modified;
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.PropertyId = new SelectList(db.Properties, "PropertyId", "Name", customerRecord.PropertyId);
            //ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name", customerRecord.TypeId);
            return View(customerRecord);
        }

        // GET: Maintenance/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRecord customerRecord = new CustomerRecord();
            //CustomerRecord customerRecord = await db.CustomerRecords.FindAsync(id);
            //if (customerRecord == null)
            //{
            //    return HttpNotFound();
            //}
            return View(customerRecord);
        }

        // POST: Maintenance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            //CustomerRecord customerRecord = await db.CustomerRecords.FindAsync(id);
            //db.CustomerRecords.Remove(customerRecord);
            //await db.SaveChangesAsync();
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
    }
}