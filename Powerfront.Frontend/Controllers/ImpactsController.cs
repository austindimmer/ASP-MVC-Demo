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
using AutoMapper;
using Newtonsoft.Json;
using System.IO;
using ServiceStack.Text;

namespace Powerfront.Frontend.Controllers
{
    public class ImpactsController : Controller
    {
        private ImpactDbContext db = new ImpactDbContext();
        IUnitOfWork _uow;
        ImpactService _impactService;

        public ImpactsController()
        {
            _uow = new UnitOfWork<ImpactDbContext>();
            _impactService = new ImpactService(_uow);
        }

        // GET: Impacts
        public async Task<ActionResult> Index()
        {
            var impacts = db.Impacts.Include(i => i.ImpactBeneficiaries);
            return View(await impacts.ToListAsync());
        }


        [HandleUIException]
        public async Task<JsonResult> GetBlankImpactViewModel()
        {
            ImpactViewModel impactViewModel = new ImpactViewModel();
            List<BeneficiaryGroup> beneficiaryGroups = db.BeneficiaryGroups.ToList();
            List<BeneficiaryGroupViewModel> beneficiaryGroupsViewModels = new List<BeneficiaryGroupViewModel>();
            foreach (var beneficiaryGroup in beneficiaryGroups)
            {
                var group = Mapper.Map<BeneficiaryGroup, BeneficiaryGroupViewModel>(beneficiaryGroup);
                beneficiaryGroupsViewModels.Add(group);
            }
            
            impactViewModel.BeneficiaryGroups = beneficiaryGroupsViewModels;
            //impact.ImpactName = "TESTING";
            impactViewModel.ImpactId = Guid.Empty;
            impactViewModel.StartDate = DateTime.Now;
            impactViewModel.FinishDate = DateTime.Now.AddYears(1);
            impactViewModel.SelectedBeneficiaryGroups = new List<BeneficiaryGroupViewModel>();

            string jsonData = SerializeImpactViewModel(impactViewModel);

            var jsonToReturn = Json(jsonData, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
            return jsonToReturn;

        }


        [HttpGet]
        public async Task<JsonResult> GetExistingImpactViewModel(string impactId)
        {
            Guid parsedGuid;
            Guid.TryParse(impactId, out parsedGuid);
            Impact impact = db.Impacts.Include(i => i.ImpactBeneficiaries).Where(i => i.ImpactId== parsedGuid).FirstOrDefault();


            string jsonImpactRecord;
            if (impact == null)
            {
                return Json(new { error = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ImpactViewModel impactViewModel = Mapper.Map<Impact, ImpactViewModel>(impact);
                impactViewModel.SelectedBeneficiaryGroups = new List<BeneficiaryGroupViewModel>();
                impactViewModel.BeneficiaryGroups = new List<BeneficiaryGroupViewModel>();

                //now I need to switch around BenefitGroups and Selected Benefit Groups for the viewModel

                var beneficiaryGroups = await db.BeneficiaryGroups.ToListAsync();
                foreach (var impactBeneficiary in impact.ImpactBeneficiaries)
                {
                    var group = Mapper.Map<BeneficiaryGroup, BeneficiaryGroupViewModel>(impactBeneficiary.BeneficiaryGroup);
                    impactViewModel.SelectedBeneficiaryGroups.Add(group);
                }

                var nonSelectedBeneficiaryGroups = beneficiaryGroups.Except(beneficiaryGroups.Join(impactViewModel.SelectedBeneficiaryGroups, g => g.BeneficiaryGroupId, s => s.BeneficiaryGroupId, (g, s) => g)).ToList();

                foreach (var impactBeneficiary in nonSelectedBeneficiaryGroups)
                {
                    var group = Mapper.Map<BeneficiaryGroup, BeneficiaryGroupViewModel>(impactBeneficiary);
                    impactViewModel.BeneficiaryGroups.Add(group);
                }

                string jsonData = SerializeImpactViewModel(impactViewModel);
                var jsonToReturn = Json(jsonData, "application/json", Encoding.UTF8, JsonRequestBehavior.AllowGet);
                return jsonToReturn;
            }
        }

        private static string SerializeImpactViewModel(ImpactViewModel impactViewModel)
        {
            //Testing Json.Net and ServiceStack for comparison purposes.

            //var jsonData = TypeSerializer.SerializeToString<ImpactViewModel>(impactViewModel);

            //string TypeSerializer.SerializeToString<T>(T value)
            //void TypeSerializer.SerializeToWriter<T>(T value, TextWriter writer)

            //T TypeSerializer.DeserializeFromString<T>(string value)
            //T TypeSerializer.DeserializeFromReader<T>(TextReader reader)

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.None
            };
            var serializer = Newtonsoft.Json.JsonSerializer.Create(settings);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                serializer.Serialize(jw, impactViewModel);
                var data = sb.ToString();
            };

            string jsonData = sb.ToString();
            return jsonData;
        }

        [ActionName("CreateUpdateImpactWithPostedJson")]
        public JsonResult CreateUpdateImpactWithPostedJson([JsonBinder]ImpactViewModel createdImpact)
        {
            bool addedOrUpdatedmpact = false;
            if (ModelState.IsValid)
            {
                // May need to wrap this lot in a transaction to allow for rollback in case of errors

                Impact impact = Mapper.Map<ImpactViewModel, Impact>(createdImpact);

                // Update impact object to reflect changes.

                if (impact.ImpactId == Guid.Empty)
                {
                    //Create new impact
                    Impact insertedOrUpdatedImpact = null;
                    impact.ImpactId = Guid.NewGuid();
                    foreach (var item in createdImpact.SelectedBeneficiaryGroups)
                    {
                        ImpactBeneficiary impactBeneficiary = new ImpactBeneficiary();
                        impactBeneficiary.BeneficiaryGroupId = item.BeneficiaryGroupId;
                        impactBeneficiary.ImpactId = impact.ImpactId;
                        impactBeneficiary.id = Guid.NewGuid();
                        impact.ImpactBeneficiaries.Add(impactBeneficiary);
                    }

                    db.Impacts.Add(impact);
                    try
                    {
                        db.SaveChanges();
                        addedOrUpdatedmpact = true;
                    }
                    catch (Exception ex)
                    {
                        addedOrUpdatedmpact = false;
                    }
                }
                else {
                    //Impact impactFromService = _impactService.GetImpactByID(impact.ImpactId);
                    List<ImpactBeneficiary> ib = db.ImpactBeneficiaries.Where(i => i.ImpactId == impact.ImpactId).ToList();
                    foreach (var impactBeneficiary in ib)
                    {
                        db.ImpactBeneficiaries.Remove(impactBeneficiary);
                    }
                    db.SaveChanges();
                    Impact impactToUpdate = db.Impacts.Include(i => i.ImpactBeneficiaries).Where(i => i.ImpactId == impact.ImpactId).FirstOrDefault();
                    //Update existing impact
                    impactToUpdate.FinishDate = impact.FinishDate;
                    impactToUpdate.ImpactName = impact.ImpactName;
                    impactToUpdate.Notes = impact.Notes;
                    impactToUpdate.Other = impact.Other;
                    impactToUpdate.StartDate = impact.StartDate;

                    foreach (var item in createdImpact.SelectedBeneficiaryGroups)
                    {
                        ImpactBeneficiary impactBeneficiary = new ImpactBeneficiary();
                        impactBeneficiary.id = Guid.NewGuid();
                        impactBeneficiary.BeneficiaryGroupId = item.BeneficiaryGroupId;
                        impactBeneficiary.ImpactId = impactToUpdate.ImpactId;
                        impactToUpdate.ImpactBeneficiaries.Add(impactBeneficiary);
                    }

                    db.SaveChanges();
                    addedOrUpdatedmpact = true;

                }
                

            }
            if (addedOrUpdatedmpact)
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
