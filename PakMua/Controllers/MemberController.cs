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
using System.Web.Security;

namespace PakMua.Controllers
{
    public class MemberController : Controller
    {
        private db_rentalanEntities db = new db_rentalanEntities();        

        // GET: Member
        public ActionResult Index()
        {
            return View(db.tb_member.ToList());
        }

        // GET: Member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_member tb_member = db.tb_member.Find(id);
            if (tb_member == null)
            {
                return HttpNotFound();
            }
            return View(tb_member);
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_member,nama,alamat,no_hp")] tb_member tb_member)
        {
            if (ModelState.IsValid)
            {
                db.tb_member.Add(tb_member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb_member);
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_member tb_member = db.tb_member.Find(id);
            if (tb_member == null)
            {
                return HttpNotFound();
            }
            return View(tb_member);
        }

        // POST: Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_member,nama,alamat,no_hp")] tb_member tb_member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb_member);
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tb_member tb_member = db.tb_member.Find(id);
            if (tb_member == null)
            {
                return HttpNotFound();
            }
            return View(tb_member);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tb_member tb_member = db.tb_member.Find(id);
            db.tb_member.Remove(tb_member);
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

        public ActionResult Login(Login member)
        {
            if (Session["email"] !=null )
            {
                return RedirectToAction("Index", "Home");
            }

            var cek = db.tb_member.Where(m => m.email.Equals(member.email) && m.password.Equals(member.password)).FirstOrDefault();

            if(cek != null)
            {
                Session["email"] = cek.email;
                Session["id_member"] = cek.id_member;
                Session["role"] = cek.role;
                    
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); 
            return RedirectToAction("Login", "Member");
        }
    }
}
