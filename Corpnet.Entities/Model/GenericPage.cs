using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Corpnet.Entities;

namespace Corpnet.Entities
{
    public class GenericPage : IEntity
    {
        [Key]
        public int id { get; set; }
      //  [Required(ErrorMessage = "Main Title Required!")]
        public string maintitle { get; set; }
       // [Required(ErrorMessage = "Sub Title Required!")]
        public string subtitle { get; set; }
       // [Required(ErrorMessage = "Content Required!")]
        public string pagecontent { get; set; }
      //  [Required(ErrorMessage = "Created By Required!")]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;


    }
}
