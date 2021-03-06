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
    
    public partial class tblHashTag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblHashTag()
        {
            this.tblHashtagAds = new HashSet<tblHashtagAd>();
            this.tblHashtagLinkestans = new HashSet<tblHashtagLinkestan>();
            this.tblHashtagNews = new HashSet<tblHashtagNew>();
            this.tblHashtagPagePosts = new HashSet<tblHashtagPagePost>();
        }
    
        public int HashtagID { get; set; }
        public string HashtagName { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> Counter { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<int> LanguageID { get; set; }
        public Nullable<int> CategoryID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblHashtagAd> tblHashtagAds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblHashtagLinkestan> tblHashtagLinkestans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblHashtagNew> tblHashtagNews { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblHashtagPagePost> tblHashtagPagePosts { get; set; }
    }
}
