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
    /// Interaction logic for ListStudentPage.xaml
    /// </summary>
    public partial class ListStudentPage : FramedPage
    {
        public ListStudentPage(StudentViewModel studentViewModel) : base(studentViewModel)
        {
            InitializeComponent();
            LvStudents.ItemsSource = studentViewModel.Students;
        }
  
        private void BtnAdd_Click(object sender, RoutedEventArgs e) => Frame.Navigate(new EditStudentPage(StudentViewModel) { Frame = Frame });

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LvStudents.SelectedItem != null)
            {
                StudentViewModel.Students.Remove(LvStudents.SelectedItem as Student);
            }
        }

        private void BtnOpen_TeacherFrame(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new ListTeacherPage(new TeacherViewModel()) { Frame = Frame });
        }

        private void BtnOpen_SubjectFrame(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new ListSubjectPage(new SubjectViewModel()) { Frame = Frame });
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (LvStudents.SelectedItem != null)
            {
                Frame.Navigate(new EditStudentPage(StudentViewModel, LvStudents.SelectedItem as Student) { Frame = Frame });
            }
        }

        private void BtnOpen_TeacherStudentFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListTeacherStudentPage(new TeacherStudentViewModel()) { Frame = Frame });

        private void BtnOpen_StudentSubjectFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListStudentSubjectPage(new StudentSubjectViewModel()) { Frame = Frame });

        private void BtnOpen_TeacherSubjectFrame(object sender, RoutedEventArgs e) => Frame.Navigate(new ListTeacherSubjectPage(new TeacherSubjectViewModel()) { Frame = Frame });

    }
}
