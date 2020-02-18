using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace PakMua.Models
{
    //public List<tb_mobil> list_mobil;
    public class ListSearchMobil
    {
        [DisplayName("Waktu Ambil")]
        public DateTime waktu_ambil { get; set; }
        [DisplayName("Waktu Kembali")]
        public DateTime waktu_kembali { get; set; }
        [DisplayName("Waktu Pesan")]
        public DateTime waktu_pesan { get; set; }
        public IEnumerable<tb_mobil> list_mobil { get; set; }
        public IEnumerable<string> brand { get; set; }
        public Transmisi transmisi { get; set; }

        public String nama { get; set; }
        public String transmisi_selected { get; set; }
        public String brand_selected { get; set; }
    }

    public enum Transmisi
    {
        Manual,
        Automatic,
        CVT        
    }
}