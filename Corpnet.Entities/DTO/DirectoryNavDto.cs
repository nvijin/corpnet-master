using System;
using System.Collections.Generic;
using System.Text;

namespace Corpnet.Entities
{
   public class DirectoryNavDto
    {
        public int id { get; set; }
        public bool ShowLeftNav { get; set; } = false;
        public bool ShowContNav { get; set; } = false;
        public bool ShowBottomNav { get; set; } = false;
        public bool ShowQuickLink { get; set; } = false;
        public string CreatedBy { get; set; }

    }
}
