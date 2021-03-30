using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TimeTableWebAPI;
using TimeTableWebAPI.Models;
using TimeTableWebAPI.Services.SubjectService;

namespace TimeTableWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SubjectController : ControllerBase
    {
        private readonly AppGlobalSettings AppGlobalSettings;
        private string DataConnectionSettingsPathFile { get; set; }

        public SubjectController(IOptions<AppGlobalSettings> appGlobalSettings)
        {
            AppGlobalSettings = appGlobalSettings.Value;
            DataConnectionSettingsPathFile = AppGlobalSettings.DataConnectionSettingsPathFile;
        }

        // GET: api/Subject
        [HttpGet("{pageSize}/{pageNumber}")]
        public async Task<ActionResult<ApiReturnValue<Subjects>>> GetSubjects(int pageSize = 100, int pageNumber = 1)
        {
            var dataConnectionSettings = JsonConvert.DeserializeObject<DataConnectionSettings>(System.IO.File.ReadAllText(DataConnectionSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(dataConnectionSettings);
            var subjectService = DependencyInjector.Resolve<ISubjectService>();

            var apiReturnValue = await subjectService.GetSubject(pageSize, pageNumber);

            if (apiReturnValue == null) { return NotFound(); }

            return apiReturnValue;
        }

        // GET: api/Subject/design
        [HttpGet("{subjectName}/{pageSize}/{pageNumber}")]
        public async Task<ActionResult<ApiReturnValue<Subjects>>> GetSubjects(string subjectName, int pageSize = 100, int pageNumber = 1)
        {
            var dataConnectionSettings = JsonConvert.DeserializeObject<DataConnectionSettings>(System.IO.File.ReadAllText(DataConnectionSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(dataConnectionSettings);
            var subjectService = DependencyInjector.Resolve<ISubjectService>();

            await subjectService.CreateSampleSubjects();

            var apiReturnValue = await subjectService.GetSubject(subjectName, pageSize, pageNumber);

            if (apiReturnValue == null){   return NotFound();  }

            return apiReturnValue;
        }

        // PUT: api/Subject/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(string id, Subject subject)
        {
            if (id != subject.SubjectCode)
            {
                return BadRequest();
            }

            var dataConnectionSettings = JsonConvert.DeserializeObject<DataConnectionSettings>(System.IO.File.ReadAllText(DataConnectionSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(dataConnectionSettings);
            var subjectService = DependencyInjector.Resolve<ISubjectService>();
            var retSubject = await subjectService.UpdateSubject(id, subject);

            return NoContent();
        }

        // POST: api/Subject
        [HttpPost]
        public async Task<ActionResult<ApiReturnValue<Subjects>>> PostSubject(Subject subject)
        {
            var dataConnectionSettings = JsonConvert.DeserializeObject<DataConnectionSettings>(System.IO.File.ReadAllText(DataConnectionSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(dataConnectionSettings);
            var subjectService = DependencyInjector.Resolve<ISubjectService>();
            
            
            var apiReturnValue = await subjectService.CreateSubject(subject);

            return apiReturnValue;
        }

        // DELETE: api/Subject/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiReturnValue<Subjects>>> DeletetblSubject(string id)
        {
            var dataConnectionSettings = JsonConvert.DeserializeObject<DataConnectionSettings>(System.IO.File.ReadAllText(DataConnectionSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(dataConnectionSettings);
            var subjectService = DependencyInjector.Resolve<ISubjectService>();


            var apiReturnValue = await subjectService.DeleteSubject(id);

            return apiReturnValue;
        }

        private bool tblSubjectExists(string id)
        {
            return true;

        }
    }
}
