using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corpnet.Entities
{
    public class DocumentDto
    {
        [Required(ErrorMessage = "Document Name Required!")]
        public string DocName { get; set; }
        [Required(ErrorMessage = "Document Description Required!")]
        public string DocDescription { get; set; }
        public string Thumbnail { get; set; } = "simple-icon-doc";
        public int DocSize { get; set; }
        public string DocType { get; set; }
        public string DocPath { get; set; }
        public int Fk_Directory_id { get; set; } = 0;
        public string CreatedBy { get; set; }
    }
}
