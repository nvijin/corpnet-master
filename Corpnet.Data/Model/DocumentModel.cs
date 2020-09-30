using Corpnet.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Corpnet.Data.Model
{
   public class DocumentModel : IEntity
    {
        [Key]
        public int id { get; set; }
        public string DocName { get; set; }
        public string DocDescription { get; set; }
        public string Thumbnail { get; set; }
        public string DocType { get; set; }
        public string DocPath { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Fk_Directory_id { get; set; }
        public bool IsVisible { get; set; } = true;
    }
}
