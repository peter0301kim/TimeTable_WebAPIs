using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;
using TimeTableWebAPI.Services.LecturerService;

namespace TimeTableWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturerSampleDataController : ControllerBase
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();

        private readonly AppGlobalSettings AppGlobalSettings;
        private string InterfaceSettingsPathFile { get; set; }
        private ILecturerService LecturerService { get; set; }


        public LecturerSampleDataController(IOptions<AppGlobalSettings> appGlobalSettings)
        {
            AppGlobalSettings = appGlobalSettings.Value;
            InterfaceSettingsPathFile = Path.Combine(AppGlobalSettings.CurrentDirectory, AppGlobalSettings.InterfaceSettingsFileName);

            var interfacesettings = JsonConvert.DeserializeObject<InterfaceSettings>(System.IO.File.ReadAllText(InterfaceSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(interfacesettings);

            LecturerService = DependencyInjector.Resolve<ILecturerService>();
        }

        // GET: api/Lecturer
        [HttpGet]
        public async Task<ActionResult<ApiReturnValue<Lecturers>>> GetLecturer()
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            try
            {
                apiReturnValue = await LecturerService.GetLecturer(0,0);
            }
            catch (Exception e)
            {
                apiReturnValue = null;
                Log.Error(e);
            }

            return apiReturnValue;
        }


        // POST: api/Lecturer
        [HttpPost]
        public async Task<ActionResult<ApiReturnValue<Lecturers>>> PostLecturer()
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            try
            {
                apiReturnValue = await LecturerService.CreateSampleLecturer();
                apiReturnValue.IsSuccess = true;
            }
            catch (Exception)
            {
                apiReturnValue.IsSuccess = false;
            }

            return apiReturnValue;
        }

    }
}
