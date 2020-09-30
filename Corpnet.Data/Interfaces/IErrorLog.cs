using Corpnet.Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Data.Interfaces
{
   public interface IErrorLog
    {
        Task InsertError(ErrorLog err, CancellationToken cancellationToken);
    }
   
}
