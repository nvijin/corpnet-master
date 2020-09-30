using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corpnet.Entities
{
    public class RecentLinksDto
    {
        [Required(ErrorMessage = "Mashreq User ID Required!")]
        public string LDAPUser_id { get; set; }
        [Required(ErrorMessage = "Document Id Required!")]
        public int Fk_Document_id { get; set; }
        [Required(ErrorMessage = "Mashreq User ID Required!")]
        public string CreatedBy { get; set; }
        [Required(ErrorMessage = "Mashreq User ID Required!")]
        public string ModifiedBy { get; set; }
    }
}
