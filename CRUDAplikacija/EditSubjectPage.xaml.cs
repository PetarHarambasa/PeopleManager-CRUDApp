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
    /// Interaction logic for EditSubjectPage.xaml
    /// </summary>
    public partial class EditSubjectPage : FramedPage
    {
        private readonly Subject subject;
        public EditSubjectPage(SubjectViewModel subjectViewModel, Subject subject = null) : base(subjectViewModel)
        {
            InitializeComponent();
            this.subject = subject ?? new Subject();
            DataContext = subject;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => Frame.NavigationService.GoBack();

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                subject.Name = TbName.Text.Trim();
                if (subject.IDSubject == 0)
                {
                    SubjectViewModel.Subjects.Add(subject);
                }
                else
                {
                    SubjectViewModel.Update(subject);
                }
                Frame.NavigationService.GoBack();
            }
        }

        private bool FormValid()
        {
            bool valid = true;
            if (string.IsNullOrEmpty(TbName.Text.Trim()))
            {
                TbName.Background = Brushes.LightCoral;
                valid = false;
            }
            else
            {
                TbName.Background = Brushes.White;
            }
            return valid;
        }
    }
}
