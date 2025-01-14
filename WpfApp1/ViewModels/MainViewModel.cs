using System.Windows.Input;
using System.Collections.ObjectModel;
using WpfApp1.Models;
using WpfApp1.Helpers;
using System.Diagnostics;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Student> Students { get; set; }
        public Student SelectedStudent { get; set; }

        public ICommand AddStudentCommand { get; }
        public ICommand EditStudentCommand { get; }
        public ICommand DeleteStudentCommand { get; }

        public MainViewModel()
        {
            Students = new ObservableCollection<Student>
            {
                new Student { StudentId = "12345", Group = "Группа 1", FullName = "Иванов Иван Иванович" },
                new Student { StudentId = "54321", Group = "Группа 2", FullName = "Петров Петр Петрович" }
            };

            // Лог для проверки
            foreach (var student in Students)
            {
                Debug.WriteLine($"Студент: {student.FullName}");
            }

            AddStudentCommand = new RelayCommand(AddStudent);
            EditStudentCommand = new RelayCommand(EditStudent, CanEditOrDelete);
            DeleteStudentCommand = new RelayCommand(DeleteStudent, CanEditOrDelete);
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
                // Здесь можно вызвать окно для редактирования
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
    }
}
