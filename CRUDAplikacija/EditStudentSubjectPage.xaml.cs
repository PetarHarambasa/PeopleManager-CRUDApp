using CRUDAplikacija.Dal;
using CRUDAplikacija.Models;
using CRUDAplikacija.ViewModel;
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
    /// Interaction logic for EditStudentSubjectPage.xaml
    /// </summary>
    public partial class EditStudentSubjectPage : FramedPage
    {
        private readonly StudentSubject studentSubject;
        public EditStudentSubjectPage(StudentSubjectViewModel studentSubjectViewModel, StudentSubject selectedStudentSubject = null) : base(studentSubjectViewModel)
        {
            InitializeComponent();
            this.studentSubject = selectedStudentSubject ?? new StudentSubject();
            DataContext = selectedStudentSubject;

            Student selectedStudent = new Student();
            Subject selectedSubject = new Subject();

            RepositoryFactory.GetRepository().GetStudents().ToList().ForEach(student =>
            {
                CbStudent.Items.Add(student);

                if (selectedStudentSubject != null && student.IDStudent == selectedStudentSubject.StudentID)
                {
                    selectedStudent = student;
                }
            });

            RepositoryFactory.GetRepository().GetSubjects().ToList().ForEach(subject =>
            {
                CbSubject.Items.Add(subject);

                if (selectedStudentSubject != null && subject.IDSubject == selectedStudentSubject.SubjectID)
                {
                    selectedSubject = subject;
                }
            });

            if (selectedStudentSubject != null)
            {
                CbStudent.SelectedItem = selectedStudent;
                CbSubject.SelectedItem = selectedSubject;
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)=> Frame.NavigationService.GoBack();

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                studentSubject.StudentID = (CbStudent.SelectedItem as Student).IDStudent;
                studentSubject.SubjectID = (CbSubject.SelectedItem as Subject).IDSubject;


                if (studentSubject.IDStudentSubject == 0)
                {
                    StudentSubjectViewModel.StudentsSubjects.Add(studentSubject);
                }
                else
                {
                    studentSubject.Student = (CbStudent.SelectedItem as Student).ToString();
                    studentSubject.Subject = (CbSubject.SelectedItem as Subject).ToString();

                    StudentSubjectViewModel.Update(studentSubject);
                }

                Frame.Navigate(new ListStudentSubjectPage(new StudentSubjectViewModel()) { Frame = Frame });
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
