using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Powerfront.Backend.Impact.EntityFramework;
using Powerfront.Backend.Model;
using Powerfront.Backend.MVC;
using Powerfront.Backend.Impact.Model;
using System.Text;
using Powerfront.Backend.Repository;
using Powerfront.Backend.Impact.Services;
using static Powerfront.Backend.Model.JsonModelBinder;

namespace Powerfront.Frontend.Controllers
{
    public class ImpactsController : Controller
    {
        private ImpactDbContext db = new ImpactDbContext();
        IUnitOfWork _uow;
        ImpactService _impactService;

        // GET: Impacts
        public async Task<ActionResult> Index()
        {
            var impacts = db.Impacts.Include(i => i.BeneficiaryGroup);
            return View(await impacts.ToListAsync());
        }


        [HandleUIException]
        public async Task<JsonResult> GetBlankImpactViewModel()
        {
            ImpactViewModel impact = new ImpactViewModel();
            var beneficiaryGroups = db.BeneficiaryGroups.ToList();
            impact.BeneficiaryGroups = beneficiaryGroups;
            //impact.ImpactName = "TESTING";
            impact.StartDate = DateTime.Now;
            impact.FinishDate = DateTime.Now.AddYears(1);
            impact.SelectedBeneficiaryGroups = new List<BeneficiaryGroup>();

            var jsonImpactRecord = ImpactViewModel.Serialize(impact);

            var jsonToReturn = Json(jsonImpactRecord, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            return jsonToReturn;

        }

        [ActionName("CreateUpdateImpactWithPostedJson")]
        public JsonResult CreateUpdateImpactWithPostedJson([JsonBinder]ImpactViewModel createdImpact)
        {
            bool addedNewImpact = false;
            if (ModelState.IsValid)
            {
                Impact newImpact = new Impact();
                //var aggregateCustomer = Mapper.Map<AggregateCustomerViewModel, AggregateCustomer>(createdCustomer);

                //var dbCreationCutomer = Mapper.Map<AggregateCustomerViewModel, AggregateCustomer>(createdCustomer);

                //foreach (var item in dbCreationCutomer.CustomerDataRecords)
                //{
                //    //These properties must be nulled out to preserve EF model referential integrity
                //    item.Property = null;
                //    item.Type = null;
                //}

                //var newAggregateCustomer = _customerService.CreateCustomer(dbCreationCutomer);

                //// Now that we have created a customer object update the properties and save in Db
                //var currentProperties = _propertyService.GetAllProperties();
                //foreach (var property in currentProperties)
                //{
                //    foreach (var record in newAggregateCustomer.CustomerDataRecords)
                //    {
                //        if (record.PropertyId == property.PropertyId)
                //        {
                //            record.Value = createdCustomer.CustomerDataRecords.Where(c => c.PropertyId == property.PropertyId).Select(r => r.Value).FirstOrDefault();
                //        }
                //    }
                //}
                var insertedImpact = _impactService.CreateImpact(newImpact);
                if (insertedImpact != null)
                {
                    addedNewImpact = true;
                }
                else {
                    addedNewImpact = false ;
                }

            }
            if (addedNewImpact)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Impacts/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Impact impact = await db.Impacts.FindAsync(id);
            if (impact == null)
            {
                return HttpNotFound();
            }
            return View(impact);
        }

        // GET: Impacts/Create
        public ActionResult Create()
        {
            ViewBag.BeneficiaryGroupId = new SelectList(db.BeneficiaryGroups, "BeneficiaryGroupId", "BeneficiaryGroupDescription");
            return View();
        }

        // POST: Impacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ImpactId,Other,Notes,StartDate,FinishDate,ImpactName,BeneficiaryGroupId")] Impact impact)
        {
            if (ModelState.IsValid)
            {
                impact.ImpactId = Guid.NewGuid();
                db.Impacts.Add(impact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BeneficiaryGroupId = new SelectList(db.BeneficiaryGroups, "BeneficiaryGroupId", "BeneficiaryGroupDescription", impact.BeneficiaryGroupId);
            return View(impact);
        }

        // GET: Impacts/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Impact impact = await db.Impacts.FindAsync(id);
            if (impact == null)
            {
                return HttpNotFound();
            }
            ViewBag.BeneficiaryGroupId = new SelectList(db.BeneficiaryGroups, "BeneficiaryGroupId", "BeneficiaryGroupDescription", impact.BeneficiaryGroupId);
            return View(impact);
        }

        // POST: Impacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ImpactId,Other,Notes,StartDate,FinishDate,ImpactName,BeneficiaryGroupId")] Impact impact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(impact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BeneficiaryGroupId = new SelectList(db.BeneficiaryGroups, "BeneficiaryGroupId", "BeneficiaryGroupDescription", impact.BeneficiaryGroupId);
            return View(impact);
        }

        // GET: Impacts/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Impact impact = await db.Impacts.FindAsync(id);
            if (impact == null)
            {
                return HttpNotFound();
            }
            return View(impact);
        }

        // POST: Impacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Impact impact = await db.Impacts.FindAsync(id);
            db.Impacts.Remove(impact);
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
