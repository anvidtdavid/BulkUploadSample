using BulkUploadSample.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BulkUploadSample.Model
{
    public class Student
    {
        [Key]
        [CSVIgnore]
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
