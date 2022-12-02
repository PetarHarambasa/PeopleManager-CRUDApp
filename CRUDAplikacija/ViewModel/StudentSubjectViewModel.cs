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
    public class StudentSubjectViewModel
    {
        public ObservableCollection<StudentSubject> StudentsSubjects { get; }

        public StudentSubjectViewModel()
        {
            StudentsSubjects = new ObservableCollection<StudentSubject>(RepositoryFactory.GetRepository().GetStudentsSubjects());
            StudentsSubjects.CollectionChanged += StudentsSubjects_CollectionChanged;
        }

        private void StudentsSubjects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddStudentSubject(StudentsSubjects[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteStudentSubject(
                        e.OldItems.OfType<StudentSubject>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateStudentSubject(
                        e.NewItems.OfType<StudentSubject>().ToList()[0]);
                    break;
            }
        }
        public void Update(StudentSubject studentSubject) => StudentsSubjects[StudentsSubjects.IndexOf(studentSubject)] = studentSubject;
    }
}
