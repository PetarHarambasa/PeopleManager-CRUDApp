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
    /// Interaction logic for EditTeacherSubjectPage.xaml
    /// </summary>
    public partial class EditTeacherSubjectPage : FramedPage
    {
        private readonly TeacherSubject teacherSubject;
        public EditTeacherSubjectPage(TeacherSubjectViewModel teacherSubjectViewModel, TeacherSubject selectedTeacherSubject = null): base(teacherSubjectViewModel)
        {
            InitializeComponent();
            this.teacherSubject = selectedTeacherSubject ?? new TeacherSubject();
            DataContext = selectedTeacherSubject;

            Teacher selectedTeacher= new Teacher();
            Subject selectedSubject = new Subject();

            RepositoryFactory.GetRepository().GetTeachers().ToList().ForEach(teacher =>
            {
                CbTeacher.Items.Add(teacher);

                if (selectedTeacherSubject != null && teacher.IDTeacher == selectedTeacherSubject.TeacherID)
                {
                    selectedTeacher = teacher;
                }
            });

            RepositoryFactory.GetRepository().GetSubjects().ToList().ForEach(subject =>
            {
                CbSubject.Items.Add(subject);

                if (selectedTeacherSubject != null && subject.IDSubject == selectedTeacherSubject.SubjectID)
                {
                    selectedSubject = subject;
                }
            });

            if (selectedTeacherSubject != null)
            {
                CbTeacher.SelectedItem = selectedTeacher;
                CbSubject.SelectedItem = selectedSubject;
            }
        }

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                teacherSubject.TeacherID = (CbTeacher.SelectedItem as Teacher).IDTeacher;
                teacherSubject.SubjectID = (CbSubject.SelectedItem as Subject).IDSubject;


                if (teacherSubject.IDTeacherSubject == 0)
                {
                    TeacherSubjectViewModel.TeachersSubjects.Add(teacherSubject);
                }
                else
                {
                    teacherSubject.Teacher = (CbTeacher.SelectedItem as Teacher).ToString();
                    teacherSubject.Subject = (CbSubject.SelectedItem as Subject).ToString();

                    TeacherSubjectViewModel.Update(teacherSubject);
                }

                Frame.Navigate(new ListTeacherSubjectPage(new TeacherSubjectViewModel()) { Frame = Frame });
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

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();
    }
}
