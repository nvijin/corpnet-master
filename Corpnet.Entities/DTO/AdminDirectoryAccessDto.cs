using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Corpnet.Entities;

namespace Corpnet.Entities
{
    public class AdminDirectoryAccessDto
    {
        [Key]
        [Required(ErrorMessage = "Mashreq User ID Required!")]
        public string LDAPUser_id { get; set; }
        [Required(ErrorMessage = "Directory ID Required!")]
        public int fk_Directory_id { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
