using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTableWebAPI.Models
{
    public class TimeTable
    {
        public string Crn { get; set; }
        public string DayOfWeek { get; set; }
        public string ClassTime { get; set; }
        public string Campus { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectDesc { get; set; }
        public string ClassRoom { get; set; }
        public string LecturerId { get; set; }
        public string LecturerName { get; set; }
        public string StartTerm { get; set; }
        public string EndTerm { get; set; }

        public const string DEF_CRN = "DEF_CRN";
        public const string DEF_DAY_OF_WEEK = "DEF_DAY_OF_WEEK";
        public const string DEF_CLASS_TIME = "DEF_CLASS_TIME";
        public const string DEF_CAMPUS = "DEF_CAMPUS";
        public const string DEF_SUBJECT_CODE = "DEF_SUBJECT_CODE";
        public const string DEF_SUBJECT_DESC = "DEF_SUBJECT_DESC";
        public const string DEF_CLASS_ROOM = "DEF_CLASS_ROOM";
        public const string DEF_LECTURER_ID = "DEF_LECTURER_ID";
        public const string DEF_LECTURER_NAME = "DEF_LECTURER_NAME";
        public const string DEF_START_TERM = "DEF_START_TERM";
        public const string DEF_END_TERM = "DEF_END_TERM";

        public TimeTable() : this(DEF_CRN, DEF_DAY_OF_WEEK, DEF_CLASS_TIME, DEF_CAMPUS,
            DEF_SUBJECT_CODE, DEF_SUBJECT_DESC, DEF_CLASS_ROOM, DEF_LECTURER_ID, DEF_LECTURER_NAME, DEF_START_TERM, DEF_END_TERM)
        { }

        public TimeTable(string crn, string dayOfWeek, string classTime, string campus, string subjectCode,
                        string subjectDesc, string classRoom, string lecturerId, string lecturerName, string startTerm = "", string endTerm = "")
        {
            this.Crn = crn;
            this.DayOfWeek = dayOfWeek;
            this.ClassTime = classTime;
            this.Campus = campus;
            this.SubjectCode = subjectCode;
            this.SubjectDesc = subjectDesc;
            this.ClassRoom = classRoom;
            this.LecturerId = lecturerId;
            this.LecturerName = lecturerName;
            this.StartTerm = startTerm;
            this.EndTerm = endTerm;
        }



    }
}
