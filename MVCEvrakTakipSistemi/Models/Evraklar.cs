//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCEvrakTakipSistemi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Evraklar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Evraklar()
        {
            this.Raporlar = new HashSet<Raporlar>();
        }
    
        public int evrakId { get; set; }
        public string evrakAd { get; set; }
        public Nullable<int> perId { get; set; }
        public string evrakYol { get; set; }
        public Nullable<System.DateTime> evrakTarih { get; set; }
        public Nullable<int> evrakDurumId { get; set; }
        public Nullable<int> evrakYerId { get; set; }
    
        public virtual Durumlar Durumlar { get; set; }
        public virtual Personeller Personeller { get; set; }
        public virtual Yerler Yerler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Raporlar> Raporlar { get; set; }
    }
}
