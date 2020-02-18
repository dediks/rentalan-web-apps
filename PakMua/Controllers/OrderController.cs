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
    public class OrderController : Controller
    {
        private db_rentalanEntities db = new db_rentalanEntities();

        // GET: Order
        public ActionResult Index()
        {
            var tb_order = db.tb_order.Include(t => t.tb_member);
            return View(tb_order.ToList());
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_order tb_order = db.tb_order.Find(id);
            if (tb_order == null)
            {
                return HttpNotFound();
            }
            return View(tb_order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.id_member = new SelectList(db.tb_member, "id_member", "nama");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_order,no_invoice,tgl_order,ambil,kembali,jumlah_hari,jumlah_mobil,total_harga,id_member,status")] tb_order tb_order)
        {
            if (ModelState.IsValid)
            {
                db.tb_order.Add(tb_order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_member = new SelectList(db.tb_member, "id_member", "nama", tb_order.id_member);
            return View(tb_order);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_order tb_order = db.tb_order.Find(id);
            if (tb_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_member = new SelectList(db.tb_member, "id_member", "nama", tb_order.id_member);
            return View(tb_order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_order,no_invoice,tgl_order,ambil,kembali,jumlah_hari,jumlah_mobil,total_harga,id_member,status")] tb_order tb_order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_member = new SelectList(db.tb_member, "id_member", "nama", tb_order.id_member);
            return View(tb_order);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_order tb_order = db.tb_order.Find(id);
            if (tb_order == null)
            {
                return HttpNotFound();
            }
            return View(tb_order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_order tb_order = db.tb_order.Find(id);
            tb_order_mobil tb_order_mobil = db.tb_order_mobil.SingleOrDefault(tom => tom.id_order == id);
            tb_mobil tb_mobil = db.tb_mobil.SingleOrDefault(tm => tm.id_mobil == tb_order_mobil.id_mobil);

            tb_mobil.stok = tb_mobil.stok + tb_order.jumlah_mobil;

            db.tb_order_mobil.Remove(tb_order_mobil);
            db.tb_order.Remove(tb_order);
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

        public ActionResult Review()
        {
            var to = db.tb_order.Where(o => o.status == "PAID").ToList();

            return View(to);
        }

        public ActionResult Konfirmasi(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_order tb_order = db.tb_order.Find(id);
            if (tb_order == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_member = new SelectList(db.tb_member, "id_member", "nama", tb_order.id_member);
            return View(tb_order);
        }

        public ActionResult ConfirmReview(int? id)
        {
            var order = db.tb_order.SingleOrDefault(o => o.id_order == id);
            order.status = "CONFIRMED";

            db.SaveChanges();

            return RedirectToAction("Index");
        }

   }
}
