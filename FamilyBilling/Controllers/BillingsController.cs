using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FamilyBilling.DAL;
using FamilyBilling.Models;

namespace FamilyBilling.Controllers
{
    public class BillingsController : Controller
    {
        private BillingContext db = new BillingContext();

        // GET: Billings
        public ActionResult Index()
        {
            var billings = db.Billings.Include(b => b.Person).ToList();
            var people = db.People.ToList();
            foreach (Billing iteam in billings)
            {
                iteam.Person= people.Where(p=>p.PersonID==iteam.PersonID).SingleOrDefault();
                var ids = iteam.IncludePersonIDs.Split(',').Select(int.Parse).ToArray();
                foreach(int id in ids)
                {
                    iteam.IncludePersonName=iteam.IncludePersonName+ "," +people.Where(p=>p.PersonID==id).SingleOrDefault().Name;
                }
                iteam.IncludePersonName = iteam.IncludePersonName.Remove(0, 1);
            }
            
            return View(billings);
        }

        // GET: Billings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Billing billing = db.Billings.Find(id);
            if (billing == null)
            {
                return HttpNotFound();
            }
            billing.Person = db.People.Where(p => p.PersonID == billing.PersonID).SingleOrDefault();
            var ids = billing.IncludePersonIDs.Split(',').Select(int.Parse).ToArray();
            foreach (int personid in ids)
            {
                billing.IncludePersonName = billing.IncludePersonName + "," + db.People.Where(p => p.PersonID == personid).SingleOrDefault().Name;
            }
            billing.IncludePersonName = billing.IncludePersonName.Remove(0, 1);
            return View(billing);
        }

        // GET: Billings/Create
        public ActionResult Create()
        {
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "Name");
            ViewBag.PeopleList = GetPeople(null);
            return View();
        }

        // POST: Billings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BillingID,Amount,Date,Comments,PersonID,IncludePersonIDs")] Billing billing, FormCollection form)
        {
            billing.IncludePersonIDs= form["IncludePeople"];
            if (ModelState.IsValid)
            {
                db.Billings.Add(billing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(db.People, "PersonID", "Name", billing.PersonID);
            if (String.IsNullOrEmpty(billing.IncludePersonIDs))
            {
                ViewBag.PeopleList = GetPeople(null);
            }
            else
            {
                ViewBag.PeopleList = GetPeople(billing.IncludePersonIDs.Split(',').Select(int.Parse).ToArray());
            }
            return View(billing);
        }

        // GET: Billings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Billing billing = db.Billings.Find(id);
            if (billing == null)
            {
                return HttpNotFound();
            }
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "Name", billing.PersonID);
            if (String.IsNullOrEmpty(billing.IncludePersonIDs))
            {
                ViewBag.PeopleList = GetPeople(null);
            }
            else
            {
                ViewBag.PeopleList = GetPeople(billing.IncludePersonIDs.Split(',').Select(int.Parse).ToArray());
            }
            return View(billing);
        }

        // POST: Billings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillingID,Amount,Date,Comments,PersonID,IncludePersonIDs")] Billing billing, FormCollection form)
        {
            billing.IncludePersonIDs = form["IncludePeople"];
            if (ModelState.IsValid)
            {
                db.Entry(billing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "Name", billing.PersonID);
            if (String.IsNullOrEmpty(billing.IncludePersonIDs))
            {
                ViewBag.PeopleList = GetPeople(null);
            }
            else
            {
                ViewBag.PeopleList = GetPeople(billing.IncludePersonIDs.Split(',').Select(int.Parse).ToArray());
            }
            return View(billing);
        }

        // GET: Billings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Billing billing = db.Billings.Find(id);
            if (billing == null)
            {
                return HttpNotFound();
            }
            billing.Person = db.People.Where(p => p.PersonID == billing.PersonID).SingleOrDefault();
            var ids = billing.IncludePersonIDs.Split(',').Select(int.Parse).ToArray();
            foreach (int personid in ids)
            {
                billing.IncludePersonName = billing.IncludePersonName + "," + db.People.Where(p => p.PersonID == personid).SingleOrDefault().Name;
            }
            billing.IncludePersonName = billing.IncludePersonName.Remove(0, 1);
           
            return View(billing);
        }

        // POST: Billings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Billing billing = db.Billings.Find(id);
            db.Billings.Remove(billing);
            db.SaveChanges();
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

        private MultiSelectList GetPeople(int[] selectedValues)
        {
            if (selectedValues == null)
            {
                selectedValues = new[] { 1, 2, 3, 4 };
            }

            return new MultiSelectList(db.People, "PersonID", "Name", selectedValues);
        }

    }
}
