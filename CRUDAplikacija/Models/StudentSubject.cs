using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDAplikacija.Models
{
    public class StudentSubject
    {
        public int IDStudentSubject { get; set; }
        public int StudentID { get; set; }
        public int SubjectID { get; set; }
        public string Student { get; set; }
        public string Subject { get; set; }
    }
}
