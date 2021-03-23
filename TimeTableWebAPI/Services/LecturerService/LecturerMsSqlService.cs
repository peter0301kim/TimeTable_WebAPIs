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
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100001', 'James', 'Oliver','James@xxx.com','2020-06-08',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100001','James','Oliver','James@xxx.com','2020-07-11',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100002','Mary','Amelia','Mary@xxx.com','2020-06-01',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100003','John','Jack','John@xxx.com','2018-06-03',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100004','Patricia','Olivia','Patricia@xxx.com','2019-12-04',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100005','Robert','Harry','Robert@xxx.com','2019-01-25',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100006','Jennifer','Isla','Jennifer@xxx.com','2019-03-21',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100007','Michael','Jacob','Michael@xxx.com','2019-01-30',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100008','Linda','Emily','Linda@xxx.com','2019-02-20',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100009','William','Charlie','William@xxx.com','2019-12-24',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100010','Elizabeth','Poppy','Elizabeth@xxx.com','2019-10-14',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100011','David','Thomas','David@xxx.com','2019-10-25',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100012','Barbara','Ava','Barbara@xxx.com','2019-09-28',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100013','Richard','George','Richard@xxx.com','2019-07-22',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100013','Susan','Isabella','Susan@xxx.com','2019-05-24',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100014','Joseph','Oscar','Joseph@xxx.com','2019-04-11',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100015','Jessica','Jessica','Jessica@xxx.com','2019-03-12',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100016','Thomas','James','Thomas@xxx.com','2019-08-18',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100017','Sarah','Liny','Sarah@xxx.com','2017-07-13',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100018','Charles','William','Charles@xxx.com','2019-11-19',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100015','Karen','Smith','Karen@xxx.com','2017-09-20',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100016','Christopher','Jones','Christopher@xxx.com','2019-12-22',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100017','Nancy','Davis','Nancy@xxx.com','2016-04-09',1);
INSERT INTO Lecturer (LecturerId,GivenName,LastName,EmailAddress,InsertDate,IsUse) VALUES ('100018','Daniel','Taylor','Daniel@xxx.com','2015-01-08',1);
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

        public async Task<ApiReturnValue<Lecturers>> GetLecturer(int pageSize = 100, int pageNumber = 1)
        {
            ApiReturnValue<Lecturers> apiReturnValue = new ApiReturnValue<Lecturers>();

            try
            {
                Lecturers lecturers = new Lecturers() {
                    TotalCount = await GetLecturerCount(),
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    Rows = await GetListLecturer(pageSize, pageNumber)
                };

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


        private async Task<int> GetLecturerCount()
        {
            return await Task.Run(() =>
            {
                string query = @"SELECT count(*) as LecturerCount FROM Lecturer WHERE IsUse = 1;";

                int lecturerCount = 0;
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
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        lecturerCount = (int)reader["LecturerCount"];
                                    }
                                    Log.Info("Read Data - Success");
                                }
                                else
                                {
                                    Log.Info("No Data");
                                }
                                reader.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    throw new Exception(ex.ToString());
                }

                return lecturerCount;

            });
        }
        private async Task<List<Lecturer>> GetListLecturer(int pageSize, int pageNumber)
        {
            return await Task.Run(() =>
            {
                string query 
                = $"SELECT LecturerId, GivenName, LastName, EmailAddress"
                + "   FROM   ( SELECT    ROW_NUMBER() OVER ( ORDER BY InsertDate ) AS RowNum, * "
                +           "  FROM      Lecturer "
                +           " WHERE     IsUse = 1 "
                +           " ) AS RowConstrainedResult "
                + $" WHERE RowNum >= {(pageSize*(pageNumber-1))+1} AND RowNum <= {(pageSize*pageNumber)}"
                +  " ORDER BY RowNum";

                Log.Info($"query:{query}");

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
                                reader.Close();
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
