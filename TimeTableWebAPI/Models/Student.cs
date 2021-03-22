using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTableWebAPI.Models
{
    public class Student
    {
        public string studentId { get; set; }
        public string identityId { get; set; }
        //public AppUser Identity { get; set; }
        public string location { get; set; }
        public string locale { get; set; }
        public string gender { get; set; }

        public string getStudentId()
        {
            return studentId;
        }

        public void setStudentId(string studentId)
        {
            this.studentId = studentId;
        }

        public string getGender()
        {
            return gender;
        }

        public void setGender(string gender)
        {
            this.gender = gender;
        }









    }
}
