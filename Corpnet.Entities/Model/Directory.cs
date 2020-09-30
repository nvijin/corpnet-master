using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Corpnet.Entities;

namespace Corpnet.Entities
{
    public class Directory : IEntity
    {
        [Key]
        public int id { get; set; }

        //[Required(ErrorMessage = "Directory Name Required!")]
        public string DirName { get; set; }

        //[Required(ErrorMessage = "Directory Description Required!")]
        public string DirDescription { get; set; }

        public string Thumbnail { get; set; } = "iconsminds-folder";

       // [Required(ErrorMessage = "Directory Parent ID Required!")]
        public int Parent_id { get; set; }
        public int SortOrder { get; set; } = 0;
        public bool ShowLeftNav { get; set; } = false;
        public bool ShowContNav { get; set; } = false;
        public bool ShowBottomNav { get; set; } = false;
        public bool ShowQuickLink { get; set; } = false;

        //[Required(ErrorMessage = "Created By Required!")]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public bool IsVisible { get; set; } = true;
        public bool IsActive { get; set; } = true;


    }
}
