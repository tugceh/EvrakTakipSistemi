using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCEvrakTakipSistemi.Models
{
    public class AyrintiliRapor
    {
        public int evrakId { get; set; }
        public string evrakAd { get; set; }
        public string personelAd { get; set; }
        public DateTime tarih { get; set; }
        public string yerAd { get; set; }
        public string durumAd { get; set; }
    }
}