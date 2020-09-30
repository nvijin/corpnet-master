using System;
using System.Collections.Generic;
using System.Text;

namespace Corpnet.Entities
{
    public class Menu : BaseEntity
    {
        public Menu()
        {
            SubMenu = new List<Menu>();
        }

        public int id { get; set; }
        public string DirName { get; set; }
        public string DirDescription { get; set; }
        public string Thumbnail { get; set; }
        public int Parent_id { get; set; }
        public bool ShowLeftNav { get; set; }
        public bool ShowContNav { get; set; }
        public bool ShowBottomNav { get; set; }
        public bool ShowQuickLink { get; set; }

        public string url { get; set; }
        public List<Menu> SubMenu { get; set; }
    }
}
