using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDAplikacija.Models
{
    public class Student : Person
    {
        public int IDStudent { get; set; }
        public int YearOfStudy { get; set; }

        public override string ToString() => $"{FirstName} {LastName}, Year of study: {YearOfStudy}";
    }
}
