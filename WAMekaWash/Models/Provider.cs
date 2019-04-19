//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WAMekaWash.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Provider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Provider()
        {
            this.Local = new HashSet<Local>();
        }
    
        public int ProviderId { get; set; }
        public string BusinessName { get; set; }
        public string RUC { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Password { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Local> Local { get; set; }
    }
}
