using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corpnet.Entities
{
    public class Document : IEntity
    {
        [Key]
        //public int id { get; set; }
        //public string DocName { get; set; }
        //public string DocDescription { get; set; }
        //public string Thumbnail { get; set; }
        //public string DocType { get; set; }
        //public string DocPath { get; set; }
        //public DateTime ModifiedDate { get; set; }
        //public int Fk_Directory_id { get; set; }

        public int id { get; set; }
        //[Required(ErrorMessage = "Document Name Required!")]
        public string DocName { get; set; }
        //[Required(ErrorMessage = "Document Description Required!")]
        public string DocDescription { get; set; }
        public string Thumbnail { get; set; } = "simple-icon-doc";
        public int DocSize { get; set; }
        public string DocType { get; set; }
        public string DocPath { get; set; }

        //[Required(ErrorMessage = "Directory ID Required!")]
        public int Fk_Directory_id { get; set; }
        public int SortOrder { get; set; }

        //[Required(ErrorMessage = "Created By Required!")]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public bool IsVisible { get; set; } = true;
        public bool IsActive { get; set; } = true;

    }
}
