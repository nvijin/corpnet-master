using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Corpnet.Entities
{
    public class RecentLinks : IEntity
    {
        [Key]
        public int id { get; set; }
      //  [Required(ErrorMessage = "Mashreq User ID Required!")]
        public string LDAPUser_id { get; set; }

      //  [Required(ErrorMessage = "Directory ID Required!")]
        public int Fk_Document_id { get; set; }

      //  [Required(ErrorMessage = "Created By Required!")]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

    }
}
