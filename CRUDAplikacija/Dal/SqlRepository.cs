using CRUDAplikacija.Models;
using CRUDAplikacija.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CRUDAplikacija.Dal
{
    internal class SqlRepository : IRepository
    {
        private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        public void AddSubject(Subject subject)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Subject.Name), subject.Name);

                    SqlParameter id = new SqlParameter(
                        nameof(subject.IDSubject),
                        SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(id);
                    cmd.ExecuteNonQuery();
                    subject.IDSubject = (int)id.Value;
                }
            }
        }

        public void AddStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Student.FirstName), student.FirstName);
                    cmd.Parameters.AddWithValue(nameof(Student.LastName), student.LastName);
                    cmd.Parameters.AddWithValue(nameof(Student.YearOfStudy), student.YearOfStudy);
                    cmd.Parameters.AddWithValue(nameof(Student.Email), student.Email);

                    cmd.Parameters.Add(new SqlParameter(nameof(Student.Picture), SqlDbType.Binary, student.Picture.Length)
                    {
                        Value = student.Picture
                    });
                    SqlParameter id = new SqlParameter(nameof(Student.IDStudent), SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(id);
                    cmd.ExecuteNonQuery();
                    student.IDStudent = (int)id.Value;
                }
            }
        }

        public void AddTeacher(Teacher teacher)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Teacher.FirstName), teacher.FirstName);
                    cmd.Parameters.AddWithValue(nameof(Teacher.LastName), teacher.LastName);
                    cmd.Parameters.AddWithValue(nameof(Teacher.Email), teacher.Email);

                    cmd.Parameters.Add(new SqlParameter(
                        nameof(Teacher.Picture),
                        SqlDbType.Binary,
                        teacher.Picture.Length)
                    {
                        Value = teacher.Picture
                    });
                    SqlParameter id = new SqlParameter(
                        nameof(Teacher.IDTeacher),
                        SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(id);
                    cmd.ExecuteNonQuery();
                    teacher.IDTeacher = (int)id.Value;
                }
            }
        }

        public void DeleteSubject(Subject subject)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Subject.IDSubject), subject.IDSubject);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Student.IDStudent), student.IDStudent);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTeacher(Teacher teacher)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Teacher.IDTeacher), teacher.IDTeacher);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<Subject> GetSubjects()
        {
            IList<Subject> list = new List<Subject>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(ReadSubject(dr));
                        }
                    }
                }
            }
            return list;
        }

        private Subject ReadSubject(SqlDataReader dr)
        => new Subject
        {
            IDSubject = (int)dr[nameof(Subject.IDSubject)],
            Name = dr[nameof(Subject.Name)].ToString()
        };

        public IList<Student> GetStudents()
        {
            IList<Student> list = new List<Student>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(ReadStudent(dr));
                        }
                    }

                }
            }
            return list;
        }

        private Student ReadStudent(SqlDataReader dr)
        => new Student
        {
            IDStudent = (int)dr[nameof(Student.IDStudent)],
            FirstName = dr[nameof(Student.FirstName)].ToString(),
            LastName = dr[nameof(Student.LastName)].ToString(),
            Email = dr[nameof(Student.Email)].ToString(),
            YearOfStudy = (int)dr[nameof(Student.YearOfStudy)],
            Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, 5)
        };

        public IList<Teacher> GetTeachers()
        {
            IList<Teacher> list = new List<Teacher>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(ReadTeacher(dr));
                        }
                    }

                }
            }
            return list;
        }

        private Teacher ReadTeacher(SqlDataReader dr)
        => new Teacher
        {
            IDTeacher = (int)dr[nameof(Teacher.IDTeacher)],
            FirstName = dr[nameof(Teacher.FirstName)].ToString(),
            LastName = dr[nameof(Teacher.LastName)].ToString(),
            Email = dr[nameof(Teacher.Email)].ToString(),
            Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, 4)
        };


        public Subject GetSubject(int idSubject)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Subject.IDSubject), idSubject);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ReadSubject(dr);
                        }
                    }
                }
            }
            throw new Exception("Wrong id");
        }

        public Student GetStudent(int idStudent)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Student.IDStudent), idStudent);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ReadStudent(dr);
                        }
                    }

                }
            }
            throw new Exception("Wrong id");
        }

        public Teacher GetTeacher(int idTeacher)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Teacher.IDTeacher), idTeacher);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ReadTeacher(dr);
                        }
                    }

                }
            }
            throw new Exception("Wrong id");
        }

        public void UpdateSubject(Subject subject)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Subject.Name), subject.Name);
                    cmd.Parameters.AddWithValue(nameof(Subject.IDSubject), subject.IDSubject);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Student.FirstName), student.FirstName);
                    cmd.Parameters.AddWithValue(nameof(Student.LastName), student.LastName);
                    cmd.Parameters.AddWithValue(nameof(Student.YearOfStudy), student.YearOfStudy);
                    cmd.Parameters.AddWithValue(nameof(Student.Email), student.Email);
                    cmd.Parameters.AddWithValue(nameof(Student.IDStudent), student.IDStudent);

                    cmd.Parameters.Add(new SqlParameter(
                        nameof(Student.Picture),
                        SqlDbType.Binary,
                        student.Picture.Length)
                    {
                        Value = student.Picture
                    });
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTeacher(Teacher teacher)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(Teacher.FirstName), teacher.FirstName);
                    cmd.Parameters.AddWithValue(nameof(Teacher.LastName), teacher.LastName);
                    cmd.Parameters.AddWithValue(nameof(Teacher.Email), teacher.Email);
                    cmd.Parameters.AddWithValue(nameof(Teacher.IDTeacher), teacher.IDTeacher);

                    cmd.Parameters.Add(new SqlParameter(
                        nameof(Teacher.Picture),
                        SqlDbType.Binary,
                        teacher.Picture.Length)
                    {
                        Value = teacher.Picture
                    });
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void AddTeacherStudent(TeacherStudent teacherStudent)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(TeacherStudent.TeacherID), teacherStudent.TeacherID);
                    cmd.Parameters.AddWithValue(nameof(TeacherStudent.StudentID), teacherStudent.StudentID);

                    SqlParameter id = new SqlParameter(
                        nameof(teacherStudent.IDTeacherStudent),
                        SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(id);
                    cmd.ExecuteNonQuery();
                    teacherStudent.IDTeacherStudent = (int)id.Value;
                }
            }
        }

        public void UpdateTeacherStudent(TeacherStudent teacherStudent)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(TeacherStudent.TeacherID), teacherStudent.TeacherID);
                    cmd.Parameters.AddWithValue(nameof(TeacherStudent.StudentID), teacherStudent.StudentID);
                    cmd.Parameters.AddWithValue(nameof(TeacherStudent.IDTeacherStudent), teacherStudent.IDTeacherStudent);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTeacherStudent(TeacherStudent teacherStudent)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(TeacherStudent.IDTeacherStudent), teacherStudent.IDTeacherStudent);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<TeacherStudent> GetTeachersStudents()
        {
            IList<TeacherStudent> list = new List<TeacherStudent>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(ReadTeacherStudent(dr));
                        }
                    }

                }
            }
            return list;
        }

        public TeacherStudent GetTeacherStudent(int idTeacherStudent)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(TeacherStudent.IDTeacherStudent), idTeacherStudent);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ReadTeacherStudent(dr);
                        }
                    }

                }
            }
            throw new Exception("Wrong id");
        }

        private TeacherStudent ReadTeacherStudent(SqlDataReader dr)
            => new TeacherStudent
            {
                IDTeacherStudent = (int)dr[nameof(TeacherStudent.IDTeacherStudent)],
                TeacherID = (int)dr[nameof(TeacherStudent.TeacherID)],
                StudentID = (int)dr[nameof(TeacherStudent.StudentID)],
                Student = dr[nameof(TeacherStudent.Student)].ToString(),
                Teacher = dr[nameof(TeacherStudent.Teacher)].ToString()
            };

        public void AddStudentSubject(StudentSubject studentSubject)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(StudentSubject.StudentID), studentSubject.StudentID);
                    cmd.Parameters.AddWithValue(nameof(StudentSubject.SubjectID), studentSubject.SubjectID);

                    SqlParameter id = new SqlParameter(
                        nameof(studentSubject.IDStudentSubject),
                        SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(id);
                    cmd.ExecuteNonQuery();
                    studentSubject.IDStudentSubject = (int)id.Value;
                }
            }
        }

        public void UpdateStudentSubject(StudentSubject studentSubject)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(StudentSubject.StudentID), studentSubject.StudentID);
                    cmd.Parameters.AddWithValue(nameof(StudentSubject.SubjectID), studentSubject.SubjectID);
                    cmd.Parameters.AddWithValue(nameof(StudentSubject.IDStudentSubject), studentSubject.IDStudentSubject);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudentSubject(StudentSubject studentSubject)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(StudentSubject.IDStudentSubject), studentSubject.IDStudentSubject);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<StudentSubject> GetStudentsSubjects()
        {
            IList<StudentSubject> list = new List<StudentSubject>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(ReadStudentSubject(dr));
                        }
                    }
                }
            }
            return list;
        }


        public StudentSubject GetStudentSubject(int idStudentSubject)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(StudentSubject.IDStudentSubject), idStudentSubject);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ReadStudentSubject(dr);
                        }
                    }

                }
            }
            throw new Exception("Wrong id");
        }
        private StudentSubject ReadStudentSubject(SqlDataReader dr)
            => new StudentSubject
            {
                IDStudentSubject = (int)dr[nameof(StudentSubject.IDStudentSubject)],
                StudentID = (int)dr[nameof(StudentSubject.StudentID)],
                SubjectID = (int)dr[nameof(StudentSubject.SubjectID)],
                Student = dr[nameof(StudentSubject.Student)].ToString(),
                Subject = dr[nameof(StudentSubject.Subject)].ToString()
            };

        public void AddTeacherSubject(TeacherSubject teacherSubject)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(TeacherSubject.TeacherID), teacherSubject.TeacherID);
                    cmd.Parameters.AddWithValue(nameof(TeacherSubject.SubjectID), teacherSubject.SubjectID);

                    SqlParameter id = new SqlParameter(
                        nameof(teacherSubject.IDTeacherSubject),
                        SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(id);
                    cmd.ExecuteNonQuery();
                    teacherSubject.IDTeacherSubject = (int)id.Value;
                }
            }
        }

        public void UpdateTeacherSubject(TeacherSubject teacherSubject)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(TeacherSubject.TeacherID), teacherSubject.TeacherID);
                    cmd.Parameters.AddWithValue(nameof(TeacherSubject.SubjectID), teacherSubject.SubjectID);
                    cmd.Parameters.AddWithValue(nameof(TeacherSubject.IDTeacherSubject), teacherSubject.IDTeacherSubject);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTeacherSubject(TeacherSubject teacherSubject)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(TeacherSubject.IDTeacherSubject), teacherSubject.IDTeacherSubject);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<TeacherSubject> GetTeachersSubjects()
        {
            IList<TeacherSubject> list = new List<TeacherSubject>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(ReadTeacherSubject(dr));
                        }
                    }
                }
            }
            return list;
        }

        public TeacherSubject GetTeacherSubject(int idTeacherSubject)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(nameof(TeacherSubject.IDTeacherSubject), idTeacherSubject);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ReadTeacherSubject(dr);
                        }
                    }

                }
            }
            throw new Exception("Wrong id");
        }

        private TeacherSubject ReadTeacherSubject(SqlDataReader dr)
           => new TeacherSubject
           {
               IDTeacherSubject = (int)dr[nameof(TeacherSubject.IDTeacherSubject)],
               TeacherID = (int)dr[nameof(TeacherSubject.TeacherID)],
               SubjectID = (int)dr[nameof(TeacherSubject.SubjectID)],
               Teacher = dr[nameof(TeacherSubject.Teacher)].ToString(),
               Subject = dr[nameof(TeacherSubject.Subject)].ToString()
           };
    }
}
