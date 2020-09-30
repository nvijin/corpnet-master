using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Text;

namespace Corpnet.Data.Model
{
    public class DirectoryModel
    {
        [Key]
        public int id { get; set; }
        public string DirName { get; set; }
        public string DirDescription { get; set; }
        public string Thumbnail { get; set; }
        public int Parent_id { get; set; }
        public int SortOrder { get; set; }
        public bool ShowLeftNav { get; set; }
        public bool ShowContNav { get; set; }
        public bool ShowBottomNav { get; set; }
        public bool ShowQuickLink { get; set; }
        //public int Level { get; set; }

        //public string CreatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; } = DateTime.Now;
        //public string ModifiedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; } = DateTime.Now;

    }
}
