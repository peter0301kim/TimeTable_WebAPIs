using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.TimeTableService
{
    public class TimeTableMySqlService : ITimeTableService
    {
        private DataConnectionSettings DataConnectionSettings { get; set; }
        public TimeTableMySqlService(DataConnectionSettings dataConnectionSettings)
        {
            this.DataConnectionSettings = dataConnectionSettings;
        }
        public async Task<List<TimeTable>> GetTimeTables(string campusId, string roomId, string dayOfWeek)
        {

            //string connectionString = _conStrings.TafeBuddyDbContext;

            List<TimeTable> tTables = new List<TimeTable>();

            using (SqlConnection con = new SqlConnection(DataConnectionSettings.ConnectionString))
            {
                con.Open();

                string sqlTimeTable = "select a.crn as CRN, e.DayShortName as DayOfWeek, "
                    + " concat(substring(convert(varchar, b.StartTime, 8),4,5) ,'-', substring(convert(varchar, b.EndTime, 8),4,5)) as ClassTime , "
                    + " b.CampusCode as Campus, a.SubjectCode as SubjectCode , d.SubjectDescription as SubjectDesc, b.room as ClassRoom, "
                    + " c.LecturerID as LecturerId, CONCAT(c.givenName, ' ', c.LastName) AS LecturerName"
                + " from crn_detail a "
                + " join crn_session_timetable b on a.CRN = b.CRN and a.TermYearStart = b.TermYearStart and a.TermCodeStart = b.TermCodeStart"
                + " Join lecturer c on a.LECTURERID = c.LecturerID "
                + " join subject d on a.SubjectCode = d.SubjectCode "
                + " join day_of_week e on b.DayCode = e.DayCode "
                + " AND b.CampusCode like '" + campusId + "%'"
                + " AND b.room = '" + roomId + "'"
                + " AND e.DayShortName like '%" + dayOfWeek + "%'"
                + " order by ClassTime";

                using (SqlCommand command = new SqlCommand(sqlTimeTable, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TimeTable table = new TimeTable();
                            table.Crn = (string)reader["CRN"];
                            table.DayOfWeek = (string)reader["DayOfWeek"];
                            table.ClassTime = (string)reader["ClassTime"];
                            table.Campus = (string)reader["Campus"];
                            table.SubjectCode = (string)reader["SubjectCode"];
                            table.SubjectDesc = (string)reader["SubjectDesc"];
                            table.ClassRoom = (string)reader["ClassRoom"];
                            table.LecturerId = (string)reader["LecturerId"];
                            table.LecturerName = (string)reader["LecturerName"];

                            tTables.Add(table);
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }


            return tTables;
        }
    }
}
