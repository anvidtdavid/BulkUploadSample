using BulkUploadSample.Model;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkUploadSample.Services
{
    public class DataManager : IDataManager
    {
        private readonly DbContext dbContext;

        public DataManager(DemoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task BulkInsert<T>(IEnumerable<T> entities) where T : class, new()
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            dbContext.Set<T>().AddRange(entities);

            await dbContext.SaveChangesAsync();
        }
    }
}
