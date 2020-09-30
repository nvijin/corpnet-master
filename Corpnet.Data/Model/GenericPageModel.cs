using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corpnet.Data.Model
{
   public class GenericPageModel
    {
        [Key]
        public int id { get; set; }
        public string maintitle { get; set; }
        
        public string subtitle { get; }
        public string pagecontent { get; }

        //public string CreatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public string ModifiedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }

    }
}
