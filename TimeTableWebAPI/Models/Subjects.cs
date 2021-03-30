using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTableWebAPI.Models
{
    public class Subjects
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; } //It starts 1
        public int PageSize { get; set; }
        public List<Subject> Rows { get; set; }
    }
}
