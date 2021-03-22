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
    [Authorize]
    public class SubjectController : ControllerBase
    {
        private readonly AppGlobalSettings AppGlobalSettings;
        private string InterfaceSettingsPathFile { get; set; }

        public SubjectController(IOptions<AppGlobalSettings> appGlobalSettings)
        {
            AppGlobalSettings = appGlobalSettings.Value;
            InterfaceSettingsPathFile = Path.Combine(AppGlobalSettings.CurrentDirectory, AppGlobalSettings.InterfaceSettingsFileName);
        }

        // GET: api/Subject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            var interfacesettings = JsonConvert.DeserializeObject<InterfaceSettings>(System.IO.File.ReadAllText(InterfaceSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(interfacesettings);
            var subjectService = DependencyInjector.Resolve<ISubjectService>();
            var subjects = await subjectService.GetSubjects();

            if (subjects == null) { return NotFound(); }

            return subjects;
        }

        // GET: api/Subject/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubject(string id)
        {
            var interfacesettings = JsonConvert.DeserializeObject<InterfaceSettings>(System.IO.File.ReadAllText(InterfaceSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(interfacesettings);
            var subjectService = DependencyInjector.Resolve<ISubjectService>();
            var subject = await subjectService.ReadSubject(id);

            if (subject == null){   return NotFound();  }

            return subject;
        }

        // PUT: api/Subject/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(string id, Subject subject)
        {
            if (id != subject.SubjectCode)
            {
                return BadRequest();
            }

            var interfacesettings = JsonConvert.DeserializeObject<InterfaceSettings>(System.IO.File.ReadAllText(InterfaceSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(interfacesettings);
            var subjectService = DependencyInjector.Resolve<ISubjectService>();
            var retSubject = await subjectService.UpdateSubject(id, subject);

            return NoContent();
        }

        // POST: api/Subject
        [HttpPost]
        public async Task<ActionResult<Subject>> PosttblSubject(Subject subject)
        {
            var interfacesettings = JsonConvert.DeserializeObject<InterfaceSettings>(System.IO.File.ReadAllText(InterfaceSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(interfacesettings);
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
