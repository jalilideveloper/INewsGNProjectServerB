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
    
    public partial class GetByIdNews_Result
    {
        public int NewsID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
        public Nullable<System.DateTime> RegisterDate { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<long> ViewNumber { get; set; }
        public string Link { get; set; }
        public Nullable<int> LanguageID { get; set; }
        public Nullable<int> RssID { get; set; }
        public Nullable<int> LikeCount { get; set; }
        public Nullable<int> Unlike { get; set; }
        public Nullable<int> Likes { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
