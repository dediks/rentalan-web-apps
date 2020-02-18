using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PakMua;
using PakMua.Models;

namespace PakMua.Controllers
{
    public class MobilController : Controller
    {
        private db_rentalanEntities db = new db_rentalanEntities();

        // GET: Mobil
        public ActionResult Index()
        {
            return View(db.tb_mobil.ToList());
        }

        // GET: Mobil/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_mobil tb_mobil = db.tb_mobil.Find(id);
            if (tb_mobil == null)
            {
                return HttpNotFound();
            }
            return View(tb_mobil);
        }

        // GET: Mobil/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mobil/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_mobil,nama,merk,model,varian,mesin,tenaga,tempat_duduk,transmisi,ac,harga_sewa,deskripsi,stok,status,foto,ImageFile")] tb_mobil tb_mobil)
        {

            string fileName = Path.GetFileNameWithoutExtension(tb_mobil.ImageFile.FileName);
            string extension = Path.GetExtension(tb_mobil.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            tb_mobil.foto = "~/Images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
            tb_mobil.ImageFile.SaveAs(fileName); 


            if (ModelState.IsValid)
            {

                db.tb_mobil.Add(tb_mobil);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_mobil);
        }

        // GET: Mobil/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_mobil tb_mobil = db.tb_mobil.Find(id);
            if (tb_mobil == null)
            {
                return HttpNotFound();
            }
            return View(tb_mobil);
        }

        // POST: Mobil/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_mobil,nama,merk,model,varian,mesin,tenaga,tempat_duduk,transmisi,ac,harga_sewa,deskripsi,stok,status,foto,ImageFile")] tb_mobil tb_mobil)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_mobil).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_mobil);
        }

        // GET: Mobil/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_mobil tb_mobil = db.tb_mobil.Find(id);
            if (tb_mobil == null)
            {
                return HttpNotFound();
            }
            return View(tb_mobil);
        }

        // POST: Mobil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_mobil tb_mobil = db.tb_mobil.Find(id);
            db.tb_mobil.Remove(tb_mobil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariMobil(CariMobilController cariMobil)
        {
            var list_mobil = db.tb_mobil.Where((x) => x.status == "READY");

            ViewBag.list_mobil = list_mobil;

            return View(list_mobil);
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
