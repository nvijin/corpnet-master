using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Corpnet.Entities;

namespace Corpnet.Entities
{
    public class AdminDirectoryAccess : IEntity
    {
        [Key]
        public int id { get; set; }
        //[Required (ErrorMessage = "Mashreq User ID Required!")]
        public string LDAPUser_id { get; set; }
        //[Required(ErrorMessage = "Directory ID Required!")]
        public int Fk_Directory_id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

    }
}
