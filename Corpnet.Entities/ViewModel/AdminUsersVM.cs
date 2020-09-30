using System;
using System.Collections.Generic;
using System.Text;

namespace Corpnet.Entities
{
   public class AdminUsersVM
    {
        public int id { get; set; }
        public string LDAPUser_id { get; set; }
        public int fk_RoleMaster_id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
