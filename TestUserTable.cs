//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class TestUserTable
    {
        public int UserID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string designation { get; set; }
    
        public virtual TestUserTable TestUserTable1 { get; set; }
        public virtual TestUserTable TestUserTable2 { get; set; }
    }
}
