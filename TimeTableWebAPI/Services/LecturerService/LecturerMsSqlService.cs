using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.LecturerService
{
    /* Sample Data
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100001','James','Oliver','James@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100002','Mary','Amelia','Mary@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100003','John','Jack','John@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100004','Patricia','Olivia','Patricia@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100005','Robert','Harry','Robert@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100006','Jennifer','Isla','Jennifer@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100007','Michael','Jacob','Michael@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100008','Linda','Emily','Linda@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100009','William','Charlie','William@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100010','Elizabeth','Poppy','Elizabeth@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100011','David','Thomas','David@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100012','Barbara','Ava','Barbara@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100013','Richard','George','Richard@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100013','Susan','Isabella','Susan@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100014','Joseph','Oscar','Joseph@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100015','Jessica','Jessica','Jessica@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100016','Thomas','James','Thomas@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100017','Sarah','Liny','Sarah@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100018','Charles','William','Charles@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100015','Karen','Smith','Karen@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100016','Christopher','Jones','Christopher@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100017','Nancy','Davis','Nancy@xxx.com');
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress) VALUES ('100018','Daniel','Taylor','Daniel@xxx.com');
 */

    public class LecturerMsSqlService : ILecturerService
    {
        private static Logger Log = LogManager.GetCurrentClassLogger();
        public MsSqlConnectionSettings ConnectionSettings { get; set; }
        private string ConnectionFile { get; set; }
        private SqlConnection SqlConn { get; set; }
        private InterfaceSettings InterfaceSettings { get; set; }
        public LecturerMsSqlService(InterfaceSettings interfaceSettings)
        {
            InterfaceSettings = interfaceSettings;
            Lecturers = new List<Lecturer>();
        }

        private List<Lecturer> Lecturers { get; set; }

        public async Task<ApiReturnValue<Lecturers>> CreateLecturer(Lecturer lecturer)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            Lecturers.Add(lecturer);
            await Task.Delay(1000);

            return apiReturnValue;
        }

        public async Task<ApiReturnValue<Lecturers>> GetLecturer(int pageSize = 0, int pageNumber = 1)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            try
            {
                var lstLecturer = await GetListLecturer();

                Lecturers lecturers = new Lecturers();
                lecturers.TotalCount = lstLecturer.Count();

                if (pageSize == 0)
                {
                    lecturers.Rows = lstLecturer;
                }
                else
                {
                    lecturers.Rows = lstLecturer.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }

                apiReturnValue.IsSuccess = true;
                apiReturnValue.Object = lecturers;
            }
            catch (Exception e)
            {
                apiReturnValue.IsSuccess = false;
                apiReturnValue.Object = null;
                apiReturnValue.Error = new ApiReturnError() { displayMessage = "Error - GetLecturer", errorMessage = e.Message };
            }
            return apiReturnValue;
        }
        public async Task<ApiReturnValue<Lecturers>> GetLecturer(string lecturerId)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            var lstLecturer = await GetLecturerById(lecturerId);

            Lecturers lecturers = new Lecturers();
            lecturers.TotalCount = 70;
            lecturers.PageNumber = 1;
            lecturers.PageSize = 20;
            lecturers.Rows = lstLecturer;

            apiReturnValue.IsSuccess = true;
            apiReturnValue.Object = lecturers;

            await Task.Delay(1000);

            return apiReturnValue;
        }

        private async Task<List<Lecturer>> GetListLecturer()
        {
            return await Task.Run(() =>
            {

                string query = @"SELECT LecturerId, GivenName, LastName, EmailAddress FROM Lecturer;";

                List<Lecturer> lstLecturer = new List<Lecturer>();

                try
                {
                    using (SqlConnection con = new SqlConnection(InterfaceSettings.ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            con.Open();
                            Log.Info("Infinitylaw Connection success");

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                try
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            Lecturer lecturer = new Lecturer();
                                            lecturer.LecturerId = (string)reader["LecturerId"];
                                            lecturer.GivenName = (string)reader["GivenName"];
                                            lecturer.LastName = (string)reader["LastName"];
                                            lecturer.EmailAddress = (string)reader["EmailAddress"];

                                            lstLecturer.Add(lecturer);
                                        }
                                        Log.Info("Read Data - Success");
                                    }
                                    else
                                    {
                                        Log.Info("No Data");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Log.Error(ex);
                                }
                                finally
                                {
                                    reader.Close();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    throw new Exception(ex.ToString());
                }

                return lstLecturer;

            });
        }

        private async Task<List<Lecturer>> GetLecturerById(string lecturerId)
        {
            return await Task.Run(() =>
            {

                string query = $"SELECT LecturerId, GivenName, LastName, EmailAddress FROM Lecturer WHERE lecturerId = '{lecturerId}';";

                List<Lecturer> lstLecturer = new List<Lecturer>();

                try
                {
                    using (SqlConnection con = new SqlConnection(InterfaceSettings.ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            con.Open();
                            Log.Info("Infinitylaw Connection success");

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                try
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            Lecturer lecturer = new Lecturer();
                                            lecturer.LecturerId = (string)reader["LecturerId"];
                                            lecturer.GivenName = (string)reader["GivenName"];
                                            lecturer.LastName = (string)reader["LastName"];
                                            lecturer.EmailAddress = (string)reader["EmailAddress"];

                                            lstLecturer.Add(lecturer);
                                        }
                                        Log.Info("Read Data - Success");
                                    }
                                    else
                                    {
                                        Log.Info("No Data");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Log.Error(ex);
                                }
                                finally
                                {
                                    reader.Close();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }

                return lstLecturer;

            });
        }

        public async Task<ApiReturnValue<Lecturers>> UpdateLecturer(string lecturerId, Lecturer lecturer)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            await Task.Delay(1000);

            return apiReturnValue;
        }

        public async Task<ApiReturnValue<Lecturers>> DeleteLecturer(string lecturerId)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            await Task.Delay(1000);

            return apiReturnValue;
        }

        public async Task<ApiReturnValue<Lecturers>> CreateSampleLecturer()
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();


            List<Lecturer> sampleLecturers = new List<Lecturer>()
            {
                new Lecturer { LecturerId = "100001", GivenName = "Thomas", LastName = "Williams", EmailAddress = "Thomas@xxx.com" },
                new Lecturer { LecturerId = "100002", GivenName = "Robert", LastName = "Brown", EmailAddress = "Robert@xxx.com"},
                new Lecturer { LecturerId = "100003", GivenName = "John", LastName = "Wilson", EmailAddress = "John@xxx.com" },
            };
            await Task.Delay(1000);

            Lecturers = sampleLecturers;

            return apiReturnValue;
        }






    }
}
