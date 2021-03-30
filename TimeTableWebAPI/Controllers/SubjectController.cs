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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            var dataConnectionSettings = JsonConvert.DeserializeObject<DataConnectionSettings>(System.IO.File.ReadAllText(DataConnectionSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(dataConnectionSettings);
            var subjectService = DependencyInjector.Resolve<ISubjectService>();
            var subjects = await subjectService.GetSubjects();

            if (subjects == null) { return NotFound(); }

            return subjects;
        }

        // GET: api/Subject/design
        [HttpGet("{subjectName}")]
        public async Task<ActionResult<List<Subject>>> GetSubjects(string subjectName)
        {
            var dataConnectionSettings = JsonConvert.DeserializeObject<DataConnectionSettings>(System.IO.File.ReadAllText(DataConnectionSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(dataConnectionSettings);
            var subjectService = DependencyInjector.Resolve<ISubjectService>();
            var subjects = await subjectService.ReadSubjects(subjectName);

            if (subjects == null){   return NotFound();  }

            return subjects;
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
        public async Task<ActionResult<Subject>> PosttblSubject(Subject subject)
        {
            var dataConnectionSettings = JsonConvert.DeserializeObject<DataConnectionSettings>(System.IO.File.ReadAllText(DataConnectionSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(dataConnectionSettings);
            var subjectService = DependencyInjector.Resolve<ISubjectService>();
            Subject retSubject = await subjectService.CreateSubject(subject);

            return CreatedAtAction("GettblSubject", new { id = retSubject.SubjectCode }, retSubject);
        }

        // DELETE: api/Subject/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Subject>> DeletetblSubject(string id)
        {

            Subject subject = new Subject();
            return subject;
        }

        private bool tblSubjectExists(string id)
        {
            return true;

        }
    }
}
