using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uyg03.Models;

namespace uyg03.ViewModel
{
    public class SepetUrunModel
    {
        public int cartitemId { get; set; }
        public string cartitemName { get; set; }

        public virtual Sepet Sepet { get; set; }
        public virtual Urun Urun { get; set; }
    }
}