using Corpnet.Data.Interfaces;
using Corpnet.Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Data.Repos
{
   
    public class ErrorlogRepocs : Repository<ErrorLog>, IErrorLog
    {
        private DataContext _ctx;

        public ErrorlogRepocs(DataContext ctx)
           : base(ctx)
        {
            _ctx = ctx;
        }
        public async Task InsertError(ErrorLog err)
        {
            await base.AddError(err);
        }
      
    }
}
