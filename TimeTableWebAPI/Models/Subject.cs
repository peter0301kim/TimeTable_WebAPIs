using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTableWebAPI.Models
{
    public class Subject
    {
        public string SubjectCode { get; set; }
        public string SubjectDescription { get; set; }

        public const string DEF_SUBJECT_CODE = "DEF_SUBJECT_CODE";
        public const string DEF_SUBJECT_DESCRIPTION = "DEF_SUBJECT_DESCRIPTION";

        public Subject(string subjectCode, string subjectDescription)
        {
            this.SubjectCode = subjectCode;
            this.SubjectDescription = subjectDescription;
        }
        public Subject() : this(DEF_SUBJECT_CODE, DEF_SUBJECT_DESCRIPTION)
        {

        }

    }
}
