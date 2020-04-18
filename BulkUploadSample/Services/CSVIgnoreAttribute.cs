using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkUploadSample.Services
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CSVIgnoreAttribute : Attribute
    {
    }
}
