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
    
    public partial class tblUploadAdsFile
    {
        public int UploadAdsFileID { get; set; }
        public Nullable<int> AdsID { get; set; }
        public string FileAddress { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual tblAdsSchedule tblAdsSchedule { get; set; }
    }
}
