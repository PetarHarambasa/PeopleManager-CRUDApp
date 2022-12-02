using CRUDAplikacija.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CRUDAplikacija.Models
{
    public abstract class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] Picture { get; set; }
        public BitmapImage Image { get => ImageUtils.ByteArrayToBitmapImage(Picture); }
    }
}
