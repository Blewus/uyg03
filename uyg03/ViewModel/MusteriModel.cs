using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uyg03.Models;

namespace uyg03.ViewModel
{
    public class MusteriModel
    {
        public int musteriId { get; set; }
        public string musteriAdsoyad { get; set; }
        public string musteriMail { get; set; }

        public virtual Sepet Sepet { get; set; }
    }
}