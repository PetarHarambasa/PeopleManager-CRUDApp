using CRUDAplikacija.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CRUDAplikacija
{
    public class FramedPage : Page
    {
        public FramedPage(TeacherViewModel teacherViewModel)
        {
            TeacherViewModel = teacherViewModel;
        }
        public FramedPage(StudentViewModel studentViewModel)
        {
            StudentViewModel = studentViewModel;
        }
        public FramedPage(SubjectViewModel subjectViewModel)
        {
            SubjectViewModel = subjectViewModel;
        }
        public FramedPage(TeacherStudentViewModel teacherStudentViewModel)
        {
            TeacherStudentViewModel = teacherStudentViewModel;
        }
        public FramedPage(StudentSubjectViewModel studentSubjectViewModel)
        {
            StudentSubjectViewModel = studentSubjectViewModel;
        }
        public FramedPage(TeacherSubjectViewModel teacherSubjectViewModel)
        {
            TeacherSubjectViewModel = teacherSubjectViewModel;
        }

        public StudentViewModel StudentViewModel { get; }
        public TeacherViewModel TeacherViewModel { get; }
        public SubjectViewModel SubjectViewModel { get; }
        public TeacherStudentViewModel TeacherStudentViewModel { get; }
        public StudentSubjectViewModel StudentSubjectViewModel { get; }
        public TeacherSubjectViewModel TeacherSubjectViewModel { get; }
        public Frame Frame { get; set; }
    }
}
