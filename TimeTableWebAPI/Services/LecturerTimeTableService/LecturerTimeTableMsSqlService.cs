using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TimeTableWebAPI.Models;

namespace TimeTableWebAPI.Services.LecturerTimeTable
{
    public class LecturerTimeTableMsSqlService : ILecturerTimeTableService
    {
        private DataConnectionSettings DataConnectionSettings { get; set; }

        public LecturerTimeTableMsSqlService(DataConnectionSettings dataConnectionSettings)
        {
            this.DataConnectionSettings = dataConnectionSettings;
        }
        public async Task<ApiReturnValue<TimeTables>> GetLecturerTimeTables(string lecturerName, string dayOfWeeks)
        {

            var strDayOfWeeks = "(" + dayOfWeeks + ")";

            //var connectionString = _conStrings.TafeSaItStudiesContext;

            var lstTimeTables = new List<TimeTable>();

            using (var db = new AppDb(DataConnectionSettings.ConnectionString))
            {
                db.Connection.Open();

                var sqlTimeTable =
                    "select a.crn as CRN, e.DayShortName as DayOfWeek, "
                        + " concat(b.StartTime, '-', b.EndTime) as ClassTime ,"
                        + " b.CampusCode as Campus, a.SubjectCode , d.SubjectDescription as SubjectDesc, b.room as ClassRoom, "
                        + " c.LecturerID as LecturerId , concat(c.givenName, ' ', c.LastName) AS LecturerName, "
                        + " concat( a.TermYearStart, '-T', a.TermCodeStart) as StartTerm , concat(a.TermYearEnd, '-T', a.TermCodeEnd) as EndTerm "
                    + " from crn_detail a, crn_session_timetable b, lecturer c, subject d, day_of_week e "
                    + " where a.crn = b.crn "
                    + " and a.lecturerid = c.lecturerid "
                    + " and a.subjectcode = d.subjectcode "
                    + " and b.daycode = e.daycode "
                    + " and year(now()) between a.TermYearStart and a.TermYearEnd "
                    //+ " and(select termcode from term_datetime where now() between startdate and enddate) between a.termcodestart and a.termcodeend "
                    + " and b.daycode in " + strDayOfWeeks
                    + " and concat(c.givenName, ' ', c.LastName) like '%" + lecturerName + "%'"
                    + " order by b.DayCode, b.starttime; ";

                using (MySqlCommand command = new MySqlCommand(sqlTimeTable, db.Connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var table = new TimeTable
                            {
                                Crn = (string)reader["CRN"],
                                DayOfWeek = (string)reader["DayOfWeek"],
                                ClassTime = (string)reader["ClassTime"],
                                Campus = (string)reader["Campus"],
                                SubjectCode = (string)reader["SubjectCode"],
                                SubjectDesc = (string)reader["SubjectDesc"],
                                ClassRoom = (string)reader["ClassRoom"],
                                LecturerId = (string)reader["LecturerId"],
                                LecturerName = (string)reader["LecturerName"],
                                StartTerm = (string)reader["StartTerm"],
                                EndTerm = (string)reader["EndTerm"]
                            };

                            lstTimeTables.Add(table);
                        }
                    }
                    reader.Close();
                }
                db.Connection.Close();
            }


            ApiReturnValue<TimeTables> apiReturnValue = new ApiReturnValue<TimeTables>()
            {
                IsSuccess = true,
                Error = new ApiReturnError() { displayMessage = "", errorMessage = "" },
                Object = new TimeTables() { TotalCount = lstTimeTables.Count(), PageNumber = 1, PageSize = 100, Rows = lstTimeTables }
            };
            return apiReturnValue;
        }
    }
}
