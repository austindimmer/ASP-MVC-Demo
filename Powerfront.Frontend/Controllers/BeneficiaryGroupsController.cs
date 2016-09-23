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

namespace Powerfront.Frontend.Controllers
{
    public class BeneficiaryGroupsController : Controller
    {
        private ImpactEntities db = new ImpactEntities();

        // GET: BeneficiaryGroups
        public async Task<ActionResult> Index()
        {
            return View(await db.BeneficiaryGroups.ToListAsync());
        }

        // GET: BeneficiaryGroups/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeneficiaryGroup beneficiaryGroup = await db.BeneficiaryGroups.FindAsync(id);
            if (beneficiaryGroup == null)
            {
                return HttpNotFound();
            }
            return View(beneficiaryGroup);
        }

        // GET: BeneficiaryGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BeneficiaryGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BeneficiaryGroupId,BeneficiaryGroupDescription")] BeneficiaryGroup beneficiaryGroup)
        {
            if (ModelState.IsValid)
            {
                beneficiaryGroup.BeneficiaryGroupId = Guid.NewGuid();
                db.BeneficiaryGroups.Add(beneficiaryGroup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(beneficiaryGroup);
        }

        // GET: BeneficiaryGroups/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeneficiaryGroup beneficiaryGroup = await db.BeneficiaryGroups.FindAsync(id);
            if (beneficiaryGroup == null)
            {
                return HttpNotFound();
            }
            return View(beneficiaryGroup);
        }

        // POST: BeneficiaryGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BeneficiaryGroupId,BeneficiaryGroupDescription")] BeneficiaryGroup beneficiaryGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beneficiaryGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(beneficiaryGroup);
        }

        // GET: BeneficiaryGroups/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeneficiaryGroup beneficiaryGroup = await db.BeneficiaryGroups.FindAsync(id);
            if (beneficiaryGroup == null)
            {
                return HttpNotFound();
            }
            return View(beneficiaryGroup);
        }

        // POST: BeneficiaryGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            BeneficiaryGroup beneficiaryGroup = await db.BeneficiaryGroups.FindAsync(id);
            db.BeneficiaryGroups.Remove(beneficiaryGroup);
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
