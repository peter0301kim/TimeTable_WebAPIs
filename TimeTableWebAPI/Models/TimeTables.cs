using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTableWebAPI.Models
{
    public class TimeTables
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; } //It starts 1
        public int PageSize { get; set; }
        public List<TimeTable> Rows { get; set; }
    }

}
