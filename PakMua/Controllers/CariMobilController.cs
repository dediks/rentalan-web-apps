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
    public class CariMobilController : Controller
    {
        private db_rentalanEntities db = new db_rentalanEntities();

        // GET: CariMobil
        public ActionResult Index(CariMobil cm)
        {

            var lsm = new ListSearchMobil()
            {
                list_mobil = db.tb_mobil.Where(x => x.status == "READY" && x.stok != 0).ToList(),
                brand = db.tb_mobil.Select(x => x.merk).Distinct().ToList(),
            };

            lsm.waktu_ambil = cm.waktu_ambil;
            lsm.waktu_kembali = cm.waktu_kembali;

            return View(lsm);
        }

        public ActionResult GetSearchMobil(string mobil, string brand)
        {
            var lsm = new ListSearchMobil();

            if (mobil != "" && brand != "")
            {
                lsm.list_mobil = db.tb_mobil.Where(x => x.nama.Contains(mobil) && x.merk == brand && x.status == "READY" && x.stok != 0).ToList();
            }
            else if (mobil != "")
            {
                lsm.list_mobil = db.tb_mobil.Where(x => x.nama.Contains(mobil) && x.status == "READY" && x.stok != 0).ToList();
            }
            else if (brand != "")
            {
                lsm.list_mobil = db.tb_mobil.Where(x => x.merk == brand && x.status == "READY" && x.stok != 0).ToList();
            }
            else
            {
                lsm.list_mobil = db.tb_mobil.Where(x => x.status == "READY" && x.stok != 0).ToList();
            }

            return PartialView("_SearchMobil", lsm);
        }

        public ActionResult cekLogin()
        {
            if(Session["email"] == null)
            {

                return RedirectToAction("Login", "Member");
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Details(int? id)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login", "Member");
            }

            if (id != null)
            {
                tb_mobil tb_mobil = db.tb_mobil.Find(id);
                return View(tb_mobil);
            }else
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult PesanMobil(CariMobil data_order)
        {
            DateTime tgl_order = DateTime.Now;
            data_order.waktu_pesan = tgl_order;

            tb_order order = new tb_order();
            var jumlah_hari = (data_order.waktu_kembali - data_order.waktu_ambil).Days;

            order.ambil = data_order.waktu_ambil;
            order.jumlah_hari = jumlah_hari;
            order.no_invoice = data_order.no_invoice;
            order.status = "UNPAID";
            order.jumlah_mobil = data_order.jumlah;
            order.kembali = data_order.waktu_kembali;
            order.total_harga = jumlah_hari * data_order.harga * data_order.jumlah;
            order.tgl_order = tgl_order;
            order.id_member = (int)Session["id_member"];

            var order_add = db.tb_order.Add(order);

            tb_order_mobil order_mobil = new tb_order_mobil();

            order_mobil.id_mobil = data_order.id_mobil;
            order_mobil.jumlah = data_order.jumlah;
            order_mobil.tgl = tgl_order;

            var order_mobil_add = db.tb_order_mobil.Add(order_mobil);

            order_add.tb_order_mobil.Add(order_mobil_add);

            var mobil = db.tb_mobil.SingleOrDefault(m => m.id_mobil == data_order.id_mobil);

            if (mobil != null)
            {
                mobil.stok = mobil.stok - data_order.jumlah;
            }

            var save = db.SaveChanges();

            var result = "Failed";
            if (save > 0){
                result = "Success";
            }

            return Json(new { Result = result, No_Invoice = data_order.no_invoice}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HasilPesan(int? no_invoice)
        {
            var order = db.tb_order.Where(x => x.no_invoice == no_invoice.ToString()).Single();

            var order_mobil = db.tb_order_mobil.Where(om => om.id_order == order.id_order).Single();

            var mobil = db.tb_mobil.Where(m => m.id_mobil == order_mobil.id_mobil).Single();

            ViewBag.invoice = no_invoice.ToString();
            ViewBag.jumlah_hari = order.jumlah_hari;
            ViewBag.jumlah_mobil = order.jumlah_mobil;
            ViewBag.total_harga = order.total_harga;
            ViewBag.nama_mobil = mobil.nama;
            ViewBag.foto = mobil.foto;
            ViewData["id_order"] = order.id_order;

            return View();
        }

        public ActionResult PaymentResult(tb_payment data, int? id_order)
        {
            data.id_order = id_order;
            var payment_add = db.tb_payment.Add(data);


            var order = db.tb_order.SingleOrDefault(o => o.id_order == id_order);
            order.status = "PAID";

            var result = db.SaveChanges();

            var str = "Failed";
            if (result > 0)
            {
                str = "Success";
            }

            return View();
        }
    }
}
