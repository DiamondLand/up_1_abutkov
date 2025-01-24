using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Helpers;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<Student> FilteredStudents { get; set; }
        public ObservableCollection<TestResult> TestResults { get; set; } // Хранение всех результатов тестов
        public ObservableCollection<TestResult> SelectedStudentResults { get; set; } // Результаты выбранного студента
        public Student SelectedStudent { get; set; }

        public string SearchQuery { get; set; }

        public ICommand AddStudentCommand { get; }
        public ICommand EditStudentCommand { get; }
        public ICommand DeleteStudentCommand { get; }
        public ICommand StartTestCommand { get; }
        public ICommand SearchCommand { get; }

        public MainViewModel()
        {
            // Данные студентов
            Students = new ObservableCollection<Student>
            {
                new Student { StudentId = "12345", Group = "Группа 1", FullName = "Иванов Иван Иванович" },
                new Student { StudentId = "54321", Group = "Группа 2", FullName = "Петров Петр Петрович" }
            };

            FilteredStudents = new ObservableCollection<Student>(Students);

            // Пример данных тестов
            TestResults = new ObservableCollection<TestResult>
            {
                new TestResult { TestId = 1, TestDate = System.DateTime.Now, StudentId = "12345", TotalQuestions = 10, CorrectAnswers = 9, Grade = "Отлично", DurationMinutes = 15 },
                new TestResult { TestId = 2, TestDate = System.DateTime.Now, StudentId = "54321", TotalQuestions = 10, CorrectAnswers = 7, Grade = "Хорошо", DurationMinutes = 20 }
            };

            SelectedStudentResults = new ObservableCollection<TestResult>();

            AddStudentCommand = new RelayCommand(AddStudent);
            EditStudentCommand = new RelayCommand(EditStudent, CanEditOrDelete);
            DeleteStudentCommand = new RelayCommand(DeleteStudent, CanEditOrDelete);
            StartTestCommand = new RelayCommand(StartTest, CanStartTest);
            SearchCommand = new RelayCommand(ExecuteSearch);

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(SelectedStudent))
                {
                    UpdateSelectedStudentResults();
                }
            };
        }

        private void AddStudent()
        {
            var newStudent = new Student { StudentId = "00000", Group = "Новая группа", FullName = "Новый студент" };
            Students.Add(newStudent);
            FilteredStudents.Add(newStudent);
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
                FilteredStudents.Remove(SelectedStudent);
            }
        }

        private void StartTest()
        {
            if (SelectedStudent == null) return;

            var testWindow = new TestWindow(SelectedStudent.StudentId, SelectedStudent.FullName);
            testWindow.Show();
        }

        private bool CanEditOrDelete()
        {
            return SelectedStudent != null;
        }

        private bool CanStartTest()
        {
            return SelectedStudent != null;
        }

        private void ExecuteSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                FilteredStudents = new ObservableCollection<Student>(Students);
            }
            else
            {
                FilteredStudents = new ObservableCollection<Student>(
                    Students.Where(s => s.FullName.Contains(SearchQuery) || s.StudentId.Contains(SearchQuery)));
            }

            OnPropertyChanged(nameof(FilteredStudents));
        }

        private void UpdateSelectedStudentResults()
        {
            SelectedStudentResults.Clear();

            if (SelectedStudent != null)
            {
                var results = TestResults.Where(tr => tr.StudentId == SelectedStudent.StudentId);
                foreach (var result in results)
                {
                    SelectedStudentResults.Add(result);
                }
            }
        }
    }
}
