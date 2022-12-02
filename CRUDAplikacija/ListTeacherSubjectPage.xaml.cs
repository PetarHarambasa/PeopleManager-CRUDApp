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
    /// Interaction logic for ListTeacherSubjectPage.xaml
    /// </summary>
    public partial class ListTeacherSubjectPage : FramedPage
    {
        public ListTeacherSubjectPage(TeacherSubjectViewModel teacherSubjectViewModel) :base(teacherSubjectViewModel)
        {
            InitializeComponent();
            teacherSubjectViewModel.TeachersSubjects.ToList().ForEach(techerSubject =>
            {
                LvTeacherSubject.Items.Add(techerSubject);
            });
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new EditTeacherSubjectPage(TeacherSubjectViewModel) { Frame = Frame });
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvTeacherSubject.SelectedItem != null)
            {
                Frame.Navigate(new EditTeacherSubjectPage(TeacherSubjectViewModel, LvTeacherSubject.SelectedItem as TeacherSubject) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvTeacherSubject.SelectedItem != null)
            {
                TeacherSubjectViewModel.TeachersSubjects.Remove(LvTeacherSubject.SelectedItem as TeacherSubject);

                Frame.Navigate(new ListTeacherSubjectPage(TeacherSubjectViewModel) { Frame = Frame });
            }
        }

        private void BtnOpen_SubjectFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListSubjectPage(new SubjectViewModel()) { Frame = Frame });

        private void BtnOpen_TeacherFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListTeacherPage(new TeacherViewModel()) { Frame = Frame });

        private void BtnOpen_StudentFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListStudentPage(new StudentViewModel()) { Frame = Frame });

        private void BtnOpen_StudentSubjectFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListStudentSubjectPage(new StudentSubjectViewModel()) { Frame = Frame });

        private void BtnOpen_TeacherStudentFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListTeacherStudentPage(new TeacherStudentViewModel()) { Frame = Frame });

    }
}
