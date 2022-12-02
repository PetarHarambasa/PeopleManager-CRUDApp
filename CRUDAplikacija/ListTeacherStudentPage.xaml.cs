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
    /// Interaction logic for ListTeacherStudentPage.xaml
    /// </summary>
    public partial class ListTeacherStudentPage : FramedPage
    {
        public ListTeacherStudentPage(TeacherStudentViewModel teacherStudentViewModel) : base(teacherStudentViewModel)
        {
            InitializeComponent();

            teacherStudentViewModel.TeachersStudents.ToList().ForEach(teacherStudent =>
            {
                LvTeacherStudent.Items.Add(teacherStudent);
            });
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvTeacherStudent.SelectedItem != null)
            {
                TeacherStudentViewModel.TeachersStudents.Remove(LvTeacherStudent.SelectedItem as TeacherStudent);

                Frame.Navigate(new ListTeacherStudentPage(TeacherStudentViewModel) { Frame = Frame });
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvTeacherStudent.SelectedItem != null)
            {
                Frame.Navigate(new EditTeacherStudentPage(TeacherStudentViewModel, LvTeacherStudent.SelectedItem as TeacherStudent) { Frame = Frame });
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new EditTeacherStudentPage(TeacherStudentViewModel) { Frame = Frame });
        private void BtnOpen_SubjectFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListSubjectPage(new SubjectViewModel()) { Frame = Frame });

        private void BtnOpen_TeacherFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListTeacherPage(new TeacherViewModel()) { Frame = Frame });

        private void BtnOpen_StudentFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListStudentPage(new StudentViewModel()) { Frame = Frame });

        private void BtnOpen_StudentSubjectFrame(object sender, RoutedEventArgs e)=> Frame.Navigate(new ListStudentSubjectPage(new StudentSubjectViewModel()) { Frame = Frame });

        private void BtnOpen_TeacherSubjectFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListTeacherSubjectPage(new TeacherSubjectViewModel()) { Frame = Frame });

    }
}
