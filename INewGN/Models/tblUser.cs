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
    
    public partial class tblUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblUser()
        {
            this.tblAdsSchedules = new HashSet<tblAdsSchedule>();
            this.tblComments = new HashSet<tblComment>();
            this.tblLinkestans = new HashSet<tblLinkestan>();
            this.tblRateUsers = new HashSet<tblRateUser>();
            this.tblUserLoginLogs = new HashSet<tblUserLoginLog>();
            this.tblUserWallPages = new HashSet<tblUserWallPage>();
        }
    
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Tell { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string SecondryPass { get; set; }
        public string UserLinkPage { get; set; }
        public string PersonelImageUrl { get; set; }
        public string RegisterDate { get; set; }
        public string NationalityID { get; set; }
        public string PostalCode { get; set; }
        public string BussinessFieldID { get; set; }
        public string BirthDate { get; set; }
        public string NickName { get; set; }
        public Nullable<int> LanguageID { get; set; }
        public Nullable<byte> UserType { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblAdsSchedule> tblAdsSchedules { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblComment> tblComments { get; set; }
        public virtual tblLanguage tblLanguage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblLinkestan> tblLinkestans { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblRateUser> tblRateUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUserLoginLog> tblUserLoginLogs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblUserWallPage> tblUserWallPages { get; set; }
    }
}
