using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkUploadSample.Services
{
    public interface IDataManager
    {  
        Task BulkInsert<T>(IEnumerable<T> entities) where T : class, new();
    }
}
