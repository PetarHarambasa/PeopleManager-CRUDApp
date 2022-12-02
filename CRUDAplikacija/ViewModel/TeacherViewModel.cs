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
    public class TeacherViewModel
    {
        public ObservableCollection<Teacher> Teachers { get; }

        public TeacherViewModel()
        {
            Teachers = new ObservableCollection<Teacher>(RepositoryFactory.GetRepository().GetTeachers());
            Teachers.CollectionChanged += Teachers_CollectionChanged;
        }

        private void Teachers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddTeacher(Teachers[e.NewStartingIndex]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeleteTeacher(
                        e.OldItems.OfType<Teacher>().ToList()[0]);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RepositoryFactory.GetRepository().UpdateTeacher(
                        e.NewItems.OfType<Teacher>().ToList()[0]);
                    break;
            }
        }
        public void Update(Teacher teacher) => Teachers[Teachers.IndexOf(teacher)] = teacher;
    }
}
