using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTableWebAPI.Models
{
    public class Course
    {
        private string courseId;
        private string courseName;
        private decimal cost;

        public const string  DEF_COURSE_ID = "DEF_STAFF_ID";
        public const string  DEF_COURSE_NAME = "DEF_STAFF_NAME";
        public const decimal DEF_COST = 0;

        public Course() : this(DEF_COURSE_ID, DEF_COURSE_NAME, DEF_COST)
        {

        }

        public Course(string courseId, string courseName, decimal cost)
        {
            this.courseId = courseId;
            this.courseName = courseName;
            this.cost = cost;
        }

        public string getCourseId()
        {
            return courseId;
        }

        public void setCourseId(string courseId)
        {
            this.courseId = courseId;
        }

        public string getCourseName()
        {
            return courseName;
        }

        public void setCourseName(string courseName)
        {
            this.courseName = courseName;
        }

        public decimal getCost()
        {
            return cost;
        }

        public void setCost(decimal cost)
        {
            this.cost = cost;
        }

    }
}
