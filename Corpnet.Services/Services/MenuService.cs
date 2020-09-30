using System;
using System.Collections.Generic;
using System.Text;
using Corpnet.Services.Interfaces;
using AutoMapper;
using Corpnet.Entities;
using Corpnet.Data.Model;
using System.Linq;
using Newtonsoft.Json;
using Corpnet.Data.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Services.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMapper mapper;
        private readonly IDirectory _repo;

        public MenuService(IMapper mapper, IDirectory repo)
        {
            this.mapper = mapper;
            this._repo = repo;
        }

        public async Task<string> GetMainMenuAsync(string menuType)
        {
            var data = await _repo.GetMenu(menuType).ConfigureAwait(false);

            Dictionary<int, Menu> dict =
            data.Select(r => new Menu
            {
                id = r.id,
                Parent_id = r.Parent_id,
                DirName = r.DirName,
                DirDescription = r.DirDescription,
                ShowLeftNav = r.ShowLeftNav,
                ShowContNav = r.ShowContNav,
                ShowBottomNav = r.ShowBottomNav,
                ShowQuickLink = r.ShowQuickLink,
                Thumbnail = r.Thumbnail,
                url = "/app/category/" + r.id
            })
             .ToDictionary(m => m.id);


            List<Menu> rootMenu = new List<Menu>();

            foreach (var kvp in dict)
            {
                List<Menu> menu = rootMenu;
                Menu item = kvp.Value;
                if (item.Parent_id > 0)
                {
                    menu = dict[item.Parent_id].SubMenu;
                }

                menu.Add(item);
            }

            string jsonMenu = JsonConvert.SerializeObject(rootMenu, Formatting.Indented);
            return jsonMenu;
        }
    }
}
