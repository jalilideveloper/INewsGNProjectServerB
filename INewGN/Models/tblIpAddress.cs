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
    
    public partial class tblIpAddress
    {
        public int IpAddressID { get; set; }
        public string IpAddress { get; set; }
        public Nullable<System.DateTime> DateRegister { get; set; }
        public string BrowserName { get; set; }
        public string WindowsName { get; set; }
        public string BrowserVersion { get; set; }
        public Nullable<int> LocationID { get; set; }
    
        public virtual tblLocation tblLocation { get; set; }
    }
}
