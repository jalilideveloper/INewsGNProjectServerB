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
    
    public partial class GetByIdUserWallPages_Result
    {
        public int UserWallPageID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> PrivacyTypeID { get; set; }
        public Nullable<long> PageRate { get; set; }
        public Nullable<bool> Block { get; set; }
        public Nullable<bool> Confrim { get; set; }
        public Nullable<bool> RegisterDate { get; set; }
        public string Title { get; set; }
        public string TelegramLink { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string GooglePlusLink { get; set; }
        public string Description { get; set; }
        public Nullable<int> FriendCount { get; set; }
        public string PageImageUrl { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> LanguageID { get; set; }
    }
}
