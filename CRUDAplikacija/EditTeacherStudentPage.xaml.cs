using CRUDAplikacija.Dal;
using CRUDAplikacija.Models;
using CRUDAplikacija.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for EditTeacherStudentPage.xaml
    /// </summary>
    public partial class EditTeacherStudentPage : FramedPage
    {
        private readonly TeacherStudent teacherStudent;
        public EditTeacherStudentPage(TeacherStudentViewModel teacherStudentViewModel, TeacherStudent selectedTeacherStudent = null) : base(teacherStudentViewModel)
        {
            InitializeComponent();
            this.teacherStudent = selectedTeacherStudent ?? new TeacherStudent();
            DataContext = selectedTeacherStudent;

            Teacher selectedTeacher = new Teacher();
            Student selectedStudent = new Student();

            RepositoryFactory.GetRepository().GetTeachers().ToList().ForEach(teacher =>
            {
                CbTeacher.Items.Add(teacher);

                if (selectedTeacherStudent != null && teacher.IDTeacher == selectedTeacherStudent.TeacherID)
                {
                    selectedTeacher = teacher;
                }
            });

            RepositoryFactory.GetRepository().GetStudents().ToList().ForEach(student =>
            {
                CbStudent.Items.Add(student);

                if (selectedTeacherStudent != null && student.IDStudent == selectedTeacherStudent.StudentID)
                {
                    selectedStudent = student;
                }
            });

            if (selectedTeacherStudent != null)
            {
                CbTeacher.SelectedItem = selectedTeacher;
                CbStudent.SelectedItem = selectedStudent;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                teacherStudent.TeacherID = (CbTeacher.SelectedItem as Teacher).IDTeacher;
                teacherStudent.StudentID = (CbStudent.SelectedItem as Student).IDStudent;


                if (teacherStudent.IDTeacherStudent == 0)
                {
                    TeacherStudentViewModel.TeachersStudents.Add(teacherStudent);
                }
                else
                {
                    teacherStudent.Teacher = (CbTeacher.SelectedItem as Teacher).ToString();
                    teacherStudent.Student = (CbStudent.SelectedItem as Student).ToString();

                    TeacherStudentViewModel.Update(teacherStudent);
                }

                Frame.Navigate(new ListTeacherStudentPage(new TeacherStudentViewModel()) { Frame = Frame });
            }
        }

        private bool FormValid()
        {
            bool valid = true;
            GridContainer.Children.OfType<ComboBox>().ToList().ForEach(e =>
            {
                if (string.IsNullOrEmpty(e.Text.Trim()))
                {
                    e.Background = Brushes.LightCoral;
                    valid = false;
                }
                else
                {
                    e.Background = Brushes.White;
                }
            });
            return valid;
        }
    }
}
