using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corpnet.Entities
{
    public class GenericPageDto
    {
        [Required(ErrorMessage = "Main Title Required!")]
        public string maintitle { get; set; }
        [Required(ErrorMessage = "Sub Title Required!")]
        public string subtitle { get; set; }
        [Required(ErrorMessage = "Page Content Required!")]
        public string pagecontent { get; set; }
        [Required(ErrorMessage = "Created By Required!")]
        public string CreatedBy { get; set; }
    }
}
