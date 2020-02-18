using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PakMua;
using PakMua.Models;

namespace PakMua.Controllers
{
    public class PengembalianController : Controller
    {
        private db_rentalanEntities db = new db_rentalanEntities();

        // GET: Pengembalian
        public ActionResult Index()
        {
            var tb_pengembalian = db.tb_pengembalian.Include(t => t.tb_order);
            return View(tb_pengembalian.ToList());
        }

        // GET: Pengembalian/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_pengembalian tb_pengembalian = db.tb_pengembalian.Find(id);
            if (tb_pengembalian == null)
            {
                return HttpNotFound();
            }
            return View(tb_pengembalian);
        }

        // GET: Pengembalian/Create
        public ActionResult Create()
        {
            ViewBag.id_order = new SelectList(db.tb_order, "id_order", "no_invoice");
            return View();
        }

        // POST: Pengembalian/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_pengembalian,id_order,tgl_pengembalian,denda")] tb_pengembalian tb_pengembalian)
        {
            if (ModelState.IsValid)
            {
                db.tb_pengembalian.Add(tb_pengembalian);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_order = new SelectList(db.tb_order, "id_order", "no_invoice", tb_pengembalian.id_order);
            return View(tb_pengembalian);
        }

        // GET: Pengembalian/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_pengembalian tb_pengembalian = db.tb_pengembalian.Find(id);
            if (tb_pengembalian == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_order = new SelectList(db.tb_order, "id_order", "no_invoice", tb_pengembalian.id_order);
            return View(tb_pengembalian);
        }

        // POST: Pengembalian/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_pengembalian,id_order,tgl_pengembalian,denda")] tb_pengembalian tb_pengembalian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_pengembalian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_order = new SelectList(db.tb_order, "id_order", "no_invoice", tb_pengembalian.id_order);
            return View(tb_pengembalian);
        }

        // GET: Pengembalian/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_pengembalian tb_pengembalian = db.tb_pengembalian.Find(id);
            if (tb_pengembalian == null)
            {
                return HttpNotFound();
            }
            return View(tb_pengembalian);
        }

        // POST: Pengembalian/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_pengembalian tb_pengembalian = db.tb_pengembalian.Find(id);
            db.tb_pengembalian.Remove(tb_pengembalian);
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

        public JsonResult CekDenda(PengembalianModel pengembalian)
        {
            tb_order tb_order = db.tb_order.SingleOrDefault(to=> to.id_order == pengembalian.id_order);

            tb_order_mobil tom = db.tb_order_mobil.SingleOrDefault(tomm => tomm.id_order == pengembalian.id_order);
            tb_mobil tm = db.tb_mobil.SingleOrDefault(tmm => tmm.id_mobil == tom.id_mobil);

            var berapa = pengembalian.tgl_pengembalian - tb_order.kembali;
            

            return Json(new { Denda = berapa, HargaSewa = tm.harga_sewa }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Kembalikan(PengembalianModel pengembalian)
        {
            tb_pengembalian kembalikan = new tb_pengembalian();

            kembalikan.id_order = pengembalian.id_order;
            kembalikan.tgl_pengembalian = pengembalian.tgl_pengembalian;
            kembalikan.denda = pengembalian.denda;

            db.tb_pengembalian.Add(kembalikan);

            tb_order to = db.tb_order.SingleOrDefault(o => o.id_order == pengembalian.id_order);
            to.status = "READY";

            var tmb = db.tb_order_mobil.SingleOrDefault(tom => tom.id_order == pengembalian.id_order);

            var tm = db.tb_mobil.SingleOrDefault(a => a.id_mobil == tmb.id_mobil);
            tm.stok = tm.stok + to.jumlah_mobil;

            var status = db.SaveChanges();

            string str = "fail";
            if(status > 0)
            {
                str = "success";
            }

            return Json(new { Status = str }, JsonRequestBehavior.AllowGet);
        }
    }
}
