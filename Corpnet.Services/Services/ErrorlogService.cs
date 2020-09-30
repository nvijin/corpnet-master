using Corpnet.Data.Interfaces;
using Corpnet.Entities.Model;
using Corpnet.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Corpnet.Services.Services
{
   
    public class ErrorlogService : IErrorlogService
    {

        private readonly IErrorLog _repo;
        // private readonly IConfiguration Configuration;

        public ErrorlogService(IErrorLog repo)
        {
            this._repo = repo;
        }

        public async Task InsertError(string Url, string MethodName, string ErrorType, string ErrorDetails, CancellationToken cancellationToken)
        {
            ErrorLog errorLog = new ErrorLog();
            errorLog.Url = Url;
            errorLog.MethodName = MethodName;
            errorLog.ErrorType = ErrorType;
            errorLog.ErrorDetails = ErrorDetails;
            errorLog.CreatedDate = DateTime.Now;
            errorLog.IsDeleted = 0;
            await _repo.InsertError(errorLog, cancellationToken).ConfigureAwait(false);
        }

      
    }
}
