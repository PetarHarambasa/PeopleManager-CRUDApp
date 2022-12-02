using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDAplikacija.Models
{

    public class Teacher : Person
    {
        public int IDTeacher { get; set; }

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
