using System.Collections.ObjectModel;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    public static class QuestionService
    {
        public static ObservableCollection<Question> GetQuestions()
        {
            return new ObservableCollection<Question>
            {
                new Question
                {
                    Id = 1,
                    Text = "Что такое ООП?",
                    Option1 = "Объектно-ориентированное программирование",
                    Option2 = "Операция-оператор процесса",
                    Option3 = "Основная образовательная программа",
                    Option4 = "Объемное оформление проекта",
                    CorrectAnswer = "Объектно-ориентированное программирование"
                },
                new Question
                {
                    Id = 2,
                    Text = "Какой метод используется для остановки программы в C#?",
                    Option1 = "break",
                    Option2 = "return",
                    Option3 = "exit",
                    Option4 = "Console.ReadKey()",
                    CorrectAnswer = "Console.ReadKey()"
                },
                new Question
                {
                    Id = 3,
                    Text = "Что такое инкапсуляция?",
                    Option1 = "Скрытие внутренней реализации объекта",
                    Option2 = "Создание новых объектов",
                    Option3 = "Использование одного метода на разных объектах",
                    Option4 = "Объявление методов в классе",
                    CorrectAnswer = "Скрытие внутренней реализации объекта"
                },
                new Question
                {
                    Id = 4,
                    Text = "Какая команда используется для клонирования репозитория Git?",
                    Option1 = "git clone",
                    Option2 = "git pull",
                    Option3 = "git fork",
                    Option4 = "git fetch",
                    CorrectAnswer = "git clone"
                }
            };
        }
    }
}
