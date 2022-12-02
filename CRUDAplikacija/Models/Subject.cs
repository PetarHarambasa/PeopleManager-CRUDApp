using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDAplikacija.Models
{
    public class Subject
    {
        public int IDSubject { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"{Name}";
    }
}
