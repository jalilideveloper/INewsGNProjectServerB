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
    
    public partial class tblHashtagLinkestan
    {
        public int HtagLinkID { get; set; }
        public Nullable<int> LinkestanID { get; set; }
        public Nullable<int> HashtagID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> LanguageID { get; set; }
    
        public virtual tblHashTag tblHashTag { get; set; }
        public virtual tblLanguage tblLanguage { get; set; }
        public virtual tblLinkestan tblLinkestan { get; set; }
    }
}
