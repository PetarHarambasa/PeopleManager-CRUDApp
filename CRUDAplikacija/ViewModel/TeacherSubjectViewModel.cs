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
    public class TeacherSubjectViewModel
    {
        public ObservableCollection<TeacherSubject> TeachersSubjects { get; }

        public TeacherSubjectViewModel()
        {
            TeachersSubjects = new ObservableCollection<TeacherSubject>(RepositoryFactory.GetRepository().GetTeachersSubjects());
            TeachersSubjects.CollectionChanged += TeacherSubjects_CollectionChanged;
        }

        private void TeacherSubjects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddTeacherSubject(TeachersSubjects[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteTeacherSubject(
                        e.OldItems.OfType<TeacherSubject>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateTeacherSubject(
                        e.NewItems.OfType<TeacherSubject>().ToList()[0]);
                    break;
            }
        }

        public void Update(TeacherSubject teacherSubject) => TeachersSubjects[TeachersSubjects.IndexOf(teacherSubject)] = teacherSubject;
    }
}
