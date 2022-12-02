using CRUDAplikacija.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDAplikacija.Dal
{
    public interface IRepository
    {

        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(Student student);
        IList<Student> GetStudents();
        Student GetStudent(int idStudent);

        void AddTeacher(Teacher teacher);
        void UpdateTeacher(Teacher teacher);
        void DeleteTeacher(Teacher teacher);
        IList<Teacher> GetTeachers();
        Teacher GetTeacher (int idTeacher);

        void AddSubject(Subject subject);
        void UpdateSubject(Subject subject);
        void DeleteSubject (Subject subject);
        IList<Subject> GetSubjects();
        Subject GetSubject(int idsubject);

        void AddTeacherStudent(TeacherStudent teacherStudent);
        void UpdateTeacherStudent(TeacherStudent teacherStudent);
        void DeleteTeacherStudent(TeacherStudent teacherStudent);
        IList<TeacherStudent> GetTeachersStudents();
        TeacherStudent GetTeacherStudent(int idTeacherStudent);
        
        void AddStudentSubject(StudentSubject studentSubject);
        void UpdateStudentSubject(StudentSubject studentSubject);
        void DeleteStudentSubject(StudentSubject studentSubject);
        IList<StudentSubject> GetStudentsSubjects();
        StudentSubject GetStudentSubject(int idStudentSubject); 
        
        void AddTeacherSubject(TeacherSubject teacherSubject);
        void UpdateTeacherSubject(TeacherSubject teacherSubject);
        void DeleteTeacherSubject(TeacherSubject teacherSubject);
        IList<TeacherSubject> GetTeachersSubjects();
        TeacherSubject GetTeacherSubject(int idTeacherSubject);
    }
}
