using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Powerfront.Backend.EntityFramework;

namespace Powerfront.Frontend.Controllers
{
    public class CustomerRecordsController : Controller
    {
        private PowerfrontEntities db = new PowerfrontEntities();

        // GET: CustomerRecords
        public async Task<ActionResult> Index()
        {
            var customerRecords = db.CustomerRecords.Include(c => c.Property).Include(c => c.Type);
            return View(await customerRecords.ToListAsync());
        }

        // GET: CustomerRecords/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRecord customerRecord = await db.CustomerRecords.FindAsync(id);
            if (customerRecord == null)
            {
                return HttpNotFound();
            }
            return View(customerRecord);
        }

        // GET: CustomerRecords/Create
        public ActionResult Create()
        {
            ViewBag.PropertyId = new SelectList(db.Properties, "PropertyId", "Name");
            ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name");
            return View();
        }

        // POST: CustomerRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TypeId,PropertyId,Value,CustomerId,RecordId")] CustomerRecord customerRecord)
        {
            if (ModelState.IsValid)
            {
                customerRecord.RecordId = Guid.NewGuid();
                db.CustomerRecords.Add(customerRecord);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PropertyId = new SelectList(db.Properties, "PropertyId", "Name", customerRecord.PropertyId);
            ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name", customerRecord.TypeId);
            return View(customerRecord);
        }

        // GET: CustomerRecords/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRecord customerRecord = await db.CustomerRecords.FindAsync(id);
            if (customerRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.PropertyId = new SelectList(db.Properties, "PropertyId", "Name", customerRecord.PropertyId);
            ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name", customerRecord.TypeId);
            return View(customerRecord);
        }

        // POST: CustomerRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TypeId,PropertyId,Value,CustomerId,RecordId")] CustomerRecord customerRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerRecord).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PropertyId = new SelectList(db.Properties, "PropertyId", "Name", customerRecord.PropertyId);
            ViewBag.TypeId = new SelectList(db.Types, "TypeId", "Name", customerRecord.TypeId);
            return View(customerRecord);
        }

        // GET: CustomerRecords/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerRecord customerRecord = await db.CustomerRecords.FindAsync(id);
            if (customerRecord == null)
            {
                return HttpNotFound();
            }
            return View(customerRecord);
        }

        // POST: CustomerRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            CustomerRecord customerRecord = await db.CustomerRecords.FindAsync(id);
            db.CustomerRecords.Remove(customerRecord);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
