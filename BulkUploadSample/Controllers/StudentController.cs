using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkUploadSample.Model;
using BulkUploadSample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulkUploadSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IFileDataAdaptor csvFileDataAdaptor;
        private readonly IDataManager dataManager;

        public StudentController(IFileDataAdaptor csvFileDataAdaptor, IDataManager dataManager)
        {
            this.csvFileDataAdaptor = csvFileDataAdaptor;
            this.dataManager = dataManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] FileUploadRequest model)
        {
            if (ModelState.IsValid)
            {
                var students = await csvFileDataAdaptor.ReadFromFile<Student>(model.File.OpenReadStream());

                await dataManager.BulkInsert(students);

                return Ok(students);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
