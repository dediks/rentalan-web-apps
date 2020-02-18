using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PakMua.Models
{
    public class PengembalianModel
    {
        public Nullable<int> id_order { get; set; }
        public Nullable<System.DateTime> tgl_pengembalian { get; set; }
        public Nullable<decimal> denda { get; set; }

    }
}