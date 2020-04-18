using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BulkUploadSample
{
    public class FileUploadRequest
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
