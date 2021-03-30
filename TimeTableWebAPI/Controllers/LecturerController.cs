using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using TimeTableWebAPI;
using TimeTableWebAPI.Models;
using TimeTableWebAPI.Services.LecturerService;

namespace TimeTableWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LecturerController : ControllerBase
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();

        private readonly AppGlobalSettings AppGlobalSettings;
        private string DataConnectionSettingsPathFile { get; set; }
        private ILecturerService LecturerService { get; set; }

        public IConfiguration _configuration;

        public LecturerController(IConfiguration configuration, IOptions<AppGlobalSettings> appGlobalSettings)
        {
            _configuration = configuration;

            AppGlobalSettings = appGlobalSettings.Value;


            DataConnectionSettingsPathFile = AppGlobalSettings.DataConnectionSettingsPathFile;

            var interfacesettings = JsonConvert.DeserializeObject<DataConnectionSettings>(System.IO.File.ReadAllText(DataConnectionSettingsPathFile));
            DependencyInjector.UpdateInterfaceModeDependencies(interfacesettings);
            
            LecturerService = DependencyInjector.Resolve<ILecturerService>();
        }

        // GET: api/Lecturer
        [HttpGet("{pageSize}/{pageNumber}")]
        public async Task<ActionResult<ApiReturnValue<Lecturers>>> GetLecturer(int pageSize = 100, int pageNumber = 1)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            try
            {
                apiReturnValue = await LecturerService.GetLecturer(pageSize, pageNumber);
            }
            catch (Exception e)
            {
                apiReturnValue = null;
                Log.Error(e);
            }

            return apiReturnValue;
        }

        // GET: api/Lecturer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiReturnValue<Lecturers>>> GetLecturer(string id)
        {

            var apiReturnValue = await LecturerService.GetLecturer(id);

            if (apiReturnValue == null)
            {
                return NotFound();
            }

            return apiReturnValue;
        }

        // PUT: api/Lecturer/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiReturnValue<Lecturers>>> PutLecturer(string id, Lecturer lecturer)
        {
            if (id != lecturer.LecturerId)
            {
                return BadRequest();
            }

            var apiReturnValue = await LecturerService.UpdateLecturer(id, lecturer);

            return apiReturnValue;
        }

        // POST: api/Lecturer
        [HttpPost]
        public async Task<ActionResult<ApiReturnValue<Lecturers>>> PostLecturer(Lecturer lecturer)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();
            try
            {
                apiReturnValue = await LecturerService.CreateLecturer(lecturer);
                apiReturnValue.IsSuccess = true;
            }
            catch (Exception)
            {
                apiReturnValue.IsSuccess = false;
            }

            return apiReturnValue;
        }

        // DELETE: api/Lecturer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiReturnValue<Lecturers>>> DeleteLecturer(string id)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            /*
            var tblLecturer = await _context.tblLecturer.FindAsync(id);

            if (tblLecturer == null)
            {
                return NotFound();
            }
            */

            Lecturer lecturer = new Lecturer();

            return apiReturnValue;
        }

        private bool tblLecturerExists(string id)
        {

            return true;
        }
    }
}
