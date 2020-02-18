using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PakMua.Models
{
    public class Login
    {
        public int id_member { get; set; }
        public string nama { get; set; }
        public string alamat { get; set; }
        public string no_hp { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string role { get; set; }

    }
}