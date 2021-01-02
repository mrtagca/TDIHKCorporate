using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDIHKCorporate.Types
{
    public class Users
    {
        public string RoleName { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}