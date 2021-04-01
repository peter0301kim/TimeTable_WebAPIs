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
    //[Authorize]
    public class TimeTableController : ControllerBase
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();

        private readonly AppGlobalSettings AppGlobalSettings;
        private string DataConnectionSettingsPathFile { get; set; }

        public TimeTableController(IOptions<AppGlobalSettings> appGlobalSettings)
        {
            AppGlobalSettings = appGlobalSettings.Value;
            DataConnectionSettingsPathFile = AppGlobalSettings.DataConnectionSettingsPathFile;
        }

        // GET: 
        [HttpGet("{campus}/{classroom}/{dayOfWeek}")]
        //[HttpGet]
        public async Task<ActionResult<ApiReturnValue<TimeTables>>> GetTimeTable(string campus, string classroom, string dayOfWeek)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataConnectionSettings = JsonConvert.DeserializeObject<DataConnectionSettings>(System.IO.File.ReadAllText(DataConnectionSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(dataConnectionSettings);
            var timeTableService = DependencyInjector.Resolve<ITimeTableService>();

            var apiReturnValue = await timeTableService.GetTimeTables(campus, classroom, dayOfWeek);

            if (apiReturnValue == null) { return NotFound(); }

            return apiReturnValue;
        }



    }
}
