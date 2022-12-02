using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDAplikacija.Models
{
    public class TeacherSubject
    {
        public int IDTeacherSubject { get; set; }
        public int TeacherID { get; set; }
        public int SubjectID { get; set; }
        public string Teacher { get; set; }
        public string Subject { get; set; }
    }
}
