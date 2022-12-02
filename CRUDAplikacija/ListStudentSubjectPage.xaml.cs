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
    /// Interaction logic for ListStudentSubjectPage.xaml
    /// </summary>
    public partial class ListStudentSubjectPage : FramedPage
    {
        public ListStudentSubjectPage(StudentSubjectViewModel studentSubjectViewModel) : base(studentSubjectViewModel)
        {
            InitializeComponent();
            studentSubjectViewModel.StudentsSubjects.ToList().ForEach(studentSubejct =>
            {
                LvStudentSubject.Items.Add(studentSubejct);
            });
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new EditStudentSubjectPage(StudentSubjectViewModel) { Frame = Frame });

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvStudentSubject.SelectedItem != null)
            {
                Frame.Navigate(new EditStudentSubjectPage(StudentSubjectViewModel, LvStudentSubject.SelectedItem as StudentSubject) { Frame = Frame });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvStudentSubject.SelectedItem != null)
            {
                StudentSubjectViewModel.StudentsSubjects.Remove(LvStudentSubject.SelectedItem as StudentSubject);

                Frame.Navigate(new ListStudentSubjectPage(StudentSubjectViewModel) { Frame = Frame });
            }
        }

        private void BtnOpen_SubjectFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListSubjectPage(new SubjectViewModel()) { Frame = Frame });

        private void BtnOpen_TeacherFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListTeacherPage(new TeacherViewModel()) { Frame = Frame });

        private void BtnOpen_StudentFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListStudentPage(new StudentViewModel()) { Frame = Frame });

        private void BtnOpen_TeacherStudentFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListTeacherStudentPage(new TeacherStudentViewModel()) { Frame = Frame });

        private void BtnOpen_TeacherSubjectFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListTeacherSubjectPage(new TeacherSubjectViewModel()) { Frame = Frame });

    }
}
