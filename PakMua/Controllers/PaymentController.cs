using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PakMua;

namespace PakMua.Controllers
{
    public class PaymentController : Controller
    {
        private db_rentalanEntities db = new db_rentalanEntities();

        // GET: Payment
        public ActionResult Index()
        {
            var tb_payment = db.tb_payment.Include(t => t.tb_order);
            return View(tb_payment.ToList());
        }

        // GET: Payment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_payment tb_payment = db.tb_payment.Find(id);
            if (tb_payment == null)
            {
                return HttpNotFound();
            }
            return View(tb_payment);
        }

        // GET: Payment/Create
        public ActionResult Create()
        {
            ViewBag.id_order = new SelectList(db.tb_order, "id_order", "no_invoice");
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_payment,nama,nama_bank,no_rekening,transfer,id_order")] tb_payment tb_payment)
        {
            if (ModelState.IsValid)
            {
                db.tb_payment.Add(tb_payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_order = new SelectList(db.tb_order, "id_order", "no_invoice", tb_payment.id_order);
            return View(tb_payment);
        }

        // GET: Payment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_payment tb_payment = db.tb_payment.Find(id);
            if (tb_payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_order = new SelectList(db.tb_order, "id_order", "no_invoice", tb_payment.id_order);
            return View(tb_payment);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_payment,nama,nama_bank,no_rekening,transfer,id_order")] tb_payment tb_payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_order = new SelectList(db.tb_order, "id_order", "no_invoice", tb_payment.id_order);
            return View(tb_payment);
        }

        // GET: Payment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_payment tb_payment = db.tb_payment.Find(id);
            if (tb_payment == null)
            {
                return HttpNotFound();
            }
            return View(tb_payment);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_payment tb_payment = db.tb_payment.Find(id);
            db.tb_payment.Remove(tb_payment);
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
    }
}
