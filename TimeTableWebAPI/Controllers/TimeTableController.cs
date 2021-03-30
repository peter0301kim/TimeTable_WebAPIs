using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using TimeTableWebAPI;
using TimeTableWebAPI.Models;
using TimeTableWebAPI.Services.TimeTableService;

namespace TimeTableWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TimeTableController : ControllerBase
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();

        private readonly AppGlobalSettings AppGlobalSettings;
        private string InterfaceSettingsPathFile { get; set; }

        public TimeTableController(IOptions<AppGlobalSettings> appGlobalSettings)
        {
            AppGlobalSettings = appGlobalSettings.Value;
        }

        // GET: api/TimeTable/5
        [HttpGet]
        public async Task<IActionResult> GetTimeTable([FromRoute] string campusId, [FromRoute]string roomId, [FromRoute]string dayOfWeek)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataConnectionSettings = JsonConvert.DeserializeObject<DataConnectionSettings>(System.IO.File.ReadAllText(InterfaceSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(dataConnectionSettings);

            var timeTableService = DependencyInjector.Resolve<ITimeTableService>();

            List<TimeTable> timeTables = await timeTableService.GetTimeTables(campusId, roomId, dayOfWeek);

            return Ok(timeTables);

        }



    }
}
