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
    //[Authorize]
    public class LecturerTimeTableController : ControllerBase
    {
        private readonly AppGlobalSettings AppGlobalSettings;
        private string DataConnectionSettingsPathFile { get; set; }


        public LecturerTimeTableController(IOptions<AppGlobalSettings> appGlobalSettings)
        {
            AppGlobalSettings = appGlobalSettings.Value;
            DataConnectionSettingsPathFile = AppGlobalSettings.DataConnectionSettingsPathFile;
        }


        // GET: api/LecturerTimeTable/
        [HttpGet("{lecturerName}/{dayOfWeeks}")]
        //[HttpGet]
        public async Task<ActionResult<ApiReturnValue<TimeTables>>> GetLecturerTimeTable([FromRoute] string lecturerName, [FromRoute] string dayOfWeeks)
        //public async Task<ActionResult<ApiReturnValue<TimeTables>>> GetLecturerTimeTable(string lecturerName, string dayOfWeeks)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataConnectionSettings = JsonConvert.DeserializeObject<DataConnectionSettings>(System.IO.File.ReadAllText(DataConnectionSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(dataConnectionSettings);

            var lecturerTimeTableService = DependencyInjector.Resolve<ILecturerTimeTableService>();

            var apiReturnValue = await lecturerTimeTableService.GetLecturerTimeTables(lecturerName, dayOfWeeks);

            if (apiReturnValue == null) { return NotFound(); }

            return apiReturnValue;

        }
    }
}
