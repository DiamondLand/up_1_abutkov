using System.Collections.ObjectModel;
using WpfApp1.Models;

namespace WpfApp1.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Student> Students { get; set; }
        public ObservableCollection<Question> Questions { get; set; }
        public ObservableCollection<TestResult> TestResults { get; set; }

        public MainViewModel()
        {
            // Инициализация данных
            Students = new ObservableCollection<Student>
            {
                new Student { StudentId = "12345", Group = "Группа 1", FullName = "Иванов Иван Иванович" },
                new Student { StudentId = "54321", Group = "Группа 2", FullName = "Петров Петр Петрович" },
                // Добавьте еще студентов
            };

            Questions = new ObservableCollection<Question>
            {
                new Question { Id = 1, Type = "Один ответ", Text = "Какой цвет небо?", Option1 = "Синий", Option2 = "Зеленый", Option3 = "Красный", Option4 = "Желтый", CorrectAnswer = "Синий" },
                // Добавьте еще вопросы
            };

            TestResults = new ObservableCollection<TestResult>();
        }
    }
}
