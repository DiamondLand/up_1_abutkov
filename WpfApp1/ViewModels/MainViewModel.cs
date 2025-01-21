using System.Windows.Input;
using System.Collections.ObjectModel;
using WpfApp1.Models;
using WpfApp1.Helpers;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Student> Students { get; set; }
        public Student SelectedStudent { get; set; }

        public ICommand AddStudentCommand { get; }
        public ICommand EditStudentCommand { get; }
        public ICommand DeleteStudentCommand { get; }
        public ICommand StartTestCommand { get; }

        public MainViewModel()
        {
            Students = new ObservableCollection<Student>
            {
                new Student { StudentId = "12345", Group = "Группа 1", FullName = "Иванов Иван Иванович" },
                new Student { StudentId = "54321", Group = "Группа 2", FullName = "Петров Петр Петрович" }
            };

            AddStudentCommand = new RelayCommand(AddStudent);
            EditStudentCommand = new RelayCommand(EditStudent, CanEditOrDelete);
            DeleteStudentCommand = new RelayCommand(DeleteStudent, CanEditOrDelete);
            StartTestCommand = new RelayCommand(StartTest, CanStartTest);
        }

        private void StartTest()
        {
            if (SelectedStudent == null) return;

            var testWindow = new TestWindow(SelectedStudent.StudentId);
            testWindow.Show();
        }

        private void AddStudent()
        {
            var newStudent = new Student { StudentId = "00000", Group = "Новая группа", FullName = "Новый студент" };
            Students.Add(newStudent);
        }

        private void EditStudent()
        {
            if (SelectedStudent != null)
            {
                SelectedStudent.FullName = "Измененное имя";
            }
        }

        private void DeleteStudent()
        {
            if (SelectedStudent != null)
            {
                Students.Remove(SelectedStudent);
            }
        }

        private bool CanEditOrDelete()
        {
            return SelectedStudent != null;
        }

        private bool CanStartTest()
        {
            return SelectedStudent != null;
        }
    }
}
