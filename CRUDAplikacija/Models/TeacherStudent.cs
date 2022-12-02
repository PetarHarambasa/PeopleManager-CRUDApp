using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDAplikacija.Models
{
    public class TeacherStudent
    {
        public int IDTeacherStudent { get; set; }
        public int StudentID { get; set; }
        public int TeacherID { get; set; }
        public string Student { get; set; }
        public string Teacher { get; set; }
    }
}
