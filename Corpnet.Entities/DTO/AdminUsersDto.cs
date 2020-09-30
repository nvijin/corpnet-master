using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Corpnet.Entities;

namespace Corpnet.Entities
{
    public class AdminUsersDto
    {
        [Key]
        [Required(ErrorMessage = "Mashreq User ID Required!")]
        public string LDAPUser_id { get; set; }

        [Required(ErrorMessage = "Role ID Required!")]
        public int fk_RoleMaster_id { get; set; }
        public string CreatedBy { get; set; }

    }
}
