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
            var billings = db.Billings.Include(b => b.Person);
            return View(billings.ToList());
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
            return View(billing);
        }

        // GET: Billings/Create
        public ActionResult Create()
        {
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "Name");
            ViewBag.IncludePersonIDs = GetPeople("1,2".Split(','));
            return View();
        }

        // POST: Billings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BillingID,Amount,Date,Comments,PersonID,IncludePersonIDs")] Billing billing, FormCollection form)
        {
            billing.IncludePersonIDs= form["IncludePersonIDs"];
            if (ModelState.IsValid)
            {
                db.Billings.Add(billing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PersonID = new SelectList(db.People, "PersonID", "Name", billing.PersonID);
            ViewBag.IncludePersonIDs = GetPeople(billing.IncludePersonIDs.Split(','));
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
            ViewBag.IncludePersonIDs = GetPeople(billing.IncludePersonIDs.Split(','));
            return View(billing);
        }

        // POST: Billings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillingID,Amount,Date,Comments,PersonID,IncludePersonIDs")] Billing billing, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "Name", billing.PersonID);
            ViewBag.IncludePersonIDs = GetPeople(billing.IncludePersonIDs.Split(','));
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

        private MultiSelectList GetPeople(string[] selectedValues)
        {
            var selectedpeople = db.People.Select(c =>c.PersonID);
            return new MultiSelectList(db.People, "PersonID", "Name", selectedpeople);
        }

    }
}
