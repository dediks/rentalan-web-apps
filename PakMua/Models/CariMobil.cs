using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PakMua.Models
{
    public class CariMobil
    {
        public String nama { get; set; }
        public String transmisi_selected { get; set; }
        public String brand_selected { get; set; }
        public decimal harga { get; set;}
        public int id_mobil { get; set; }
        public int jumlah { get; set; }

        [DisplayName("Waktu Ambil")]
        public DateTime waktu_ambil { get; set; }
        [DisplayName("Waktu Kembali")]
        public DateTime waktu_kembali { get; set; }
        [DisplayName("Waktu Pesan")]
        public DateTime waktu_pesan { get; set; }

        public tb_mobil mobil { get; set; }
        public IEnumerable<tb_mobil> list_mobil { get; set; }
        public tb_order order { get; set; }
        public String no_invoice { get; set; }
    }
}