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
    
    public partial class Qualification
    {
        public int QualificationId { get; set; }
        public int Punctuation { get; set; }
        public string Detail { get; set; }
        public int CustomerId { get; set; }
        public Nullable<int> LocalId { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Local Local { get; set; }
    }
}
