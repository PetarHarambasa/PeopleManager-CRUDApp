using CRUDAplikacija.Dal;
using CRUDAplikacija.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDAplikacija.ViewModel
{
    public class TeacherStudentViewModel
    {
        public ObservableCollection<TeacherStudent> TeachersStudents { get; }

        public TeacherStudentViewModel()
        {
            TeachersStudents = new ObservableCollection<TeacherStudent>(RepositoryFactory.GetRepository().GetTeachersStudents());
            TeachersStudents.CollectionChanged += TeachersStudents_CollectionChanged;
        }

        private void TeachersStudents_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddTeacherStudent(TeachersStudents[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteTeacherStudent(
                        e.OldItems.OfType<TeacherStudent>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateTeacherStudent(
                        e.NewItems.OfType<TeacherStudent>().ToList()[0]);
                    break;
            }
        }
        public void Update(TeacherStudent teacherStudent) => TeachersStudents[TeachersStudents.IndexOf(teacherStudent)] = teacherStudent;
    }
}
