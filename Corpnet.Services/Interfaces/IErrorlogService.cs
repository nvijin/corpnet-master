using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Services.Interfaces
{
    public interface IErrorlogService
    {
        Task InsertError(string Url, string MethodName, string ErrorType, string ErrorDetails);
    }
}
