using CRUDAplikacija.Models;
using CRUDAplikacija.Utils;
using CRUDAplikacija.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRUDAplikacija
{
    /// <summary>
    /// Interaction logic for EditTeacherPage.xaml
    /// </summary>
    public partial class EditTeacherPage : FramedPage
    {
        private const string Filter = "All supported graphics|*.jpg;*.jpeg;*.png|JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|Portable Network Graphic (*.png)|*.png";
        private readonly Teacher teacher;
        public EditTeacherPage(TeacherViewModel teacherViewModel, Teacher teacher = null) : base(teacherViewModel)
        {
            InitializeComponent();
            this.teacher = teacher ?? new Teacher();
            DataContext = teacher;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                teacher.Email = TbEmail.Text.Trim();
                teacher.FirstName = TbFirstName.Text.Trim();
                teacher.LastName = TbLastName.Text.Trim();
                teacher.Picture = ImageUtils.BitmapImageToByteArray(Picture.Source as BitmapImage);
                if (teacher.IDTeacher == 0)
                {
                    TeacherViewModel.Teachers.Add(teacher);
                }
                else
                {
                    TeacherViewModel.Update(teacher);
                }
                Frame.NavigationService.GoBack();
            }
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = Filter
            };
            if (openFileDialog.ShowDialog() == true)
            {
                Picture.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private bool FormValid()
        {
            bool valid = true;
            GridContainer.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                if (string.IsNullOrEmpty(e.Text.Trim())
                    || ("Email".Equals(e.Tag) && !ValidationUtils.isValidEmail(TbEmail.Text.Trim())))
                {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                }
                else
                {
                    e.Background = Brushes.White;
                }
            });
            if (Picture.Source == null)
            {
                PictureBorder.BorderBrush = Brushes.LightCoral;
                valid = false;
            }
            else
            {
                PictureBorder.BorderBrush = Brushes.WhiteSmoke;
            }
            return valid;
        }
    }
}
