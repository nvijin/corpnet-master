using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Corpnet.Services.Interfaces
{
    public interface IMenuService
    {
        //IEnumerable<Task<string>> GetMainMenuAsync(int id);
        Task<string> GetMainMenuAsync(string menuType);
    }
}
