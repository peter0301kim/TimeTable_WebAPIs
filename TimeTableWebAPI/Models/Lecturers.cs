using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTableWebAPI.Models
{
    public class Lecturers
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; } //Starts 1
        public int PageSize { get; set; }
        public List<Lecturer> Rows { get; set; }
    }
}
