//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace INewGN.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPagePostLocation
    {
        public int PagePostLocID { get; set; }
        public Nullable<int> PagePostID { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual tblLocation tblLocation { get; set; }
        public virtual tblPagePost tblPagePost { get; set; }
    }
}