using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corpnet.Entities
{
    public class FavouriteDto
    {

        [Required(ErrorMessage = "Document/Directory Type Required!")]
        public string DocDirType { get; set; }
        [Required(ErrorMessage = "Document/Directory Id Required!")]
        public int fk_DocDir_id { get; set; }

        [Required(ErrorMessage = "Mashreq User ID Required!")]
        public string LDAPUser_id { get; set; }
    }
}
