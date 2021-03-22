using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;
using TimeTableWebAPI.Services.LecturerTimeTable;

namespace TimeTableWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LecturerTimeTableController : ControllerBase
    {
        private readonly AppGlobalSettings AppGlobalSettings;
        private string InterfaceSettingsPathFile { get; set; }


        public LecturerTimeTableController(IOptions<AppGlobalSettings> appGlobalSettings)
        {
            AppGlobalSettings = appGlobalSettings.Value;
            InterfaceSettingsPathFile = Path.Combine(AppGlobalSettings.CurrentDirectory, AppGlobalSettings.InterfaceSettingsFileName);
        }


        // GET: api/TimeTable/5
        [HttpGet]
        public async Task<IActionResult> GetLecturerTimeTable([FromRoute] string lecturerName, [FromRoute] string dayOfWeeks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var interfacesettings = JsonConvert.DeserializeObject<InterfaceSettings>(System.IO.File.ReadAllText(InterfaceSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(interfacesettings);

            var lecturerTimeTableService = DependencyInjector.Resolve<ILecturerTimeTableService>();

            List<TimeTable> timeTables = await lecturerTimeTableService.GetLecturerTimeTables(lecturerName, dayOfWeeks);

            return Ok(timeTables);

        }
    }
}
