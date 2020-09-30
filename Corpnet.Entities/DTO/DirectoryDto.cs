using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corpnet.Entities
{
    public class DirectoryDto
    {
        [Required(ErrorMessage = "Directory Name Required!")]
        public string DirName { get; set; }

        [Required(ErrorMessage = "Directory Description Required!")]
        public string DirDescription { get; set; }
        public string Thumbnail { get; set; } = "iconsminds-folder";

        [Required(ErrorMessage = "Parent_id Required!")]
        public int Parent_id { get; set; } = 0;

        [Required(ErrorMessage = "Created By Required!")]
        public string CreatedBy { get; set; }
    }
}
