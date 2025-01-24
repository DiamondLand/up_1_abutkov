using System;
using System.Windows;
using WpfApp1.Services;

namespace WpfApp1.Views
{
    public partial class TestWindow : Window
    {
        public TestWindow(string studentId, string FullName)
        {
            InitializeComponent();
            try
            {
                var questions = QuestionService.GetQuestions();
                DataContext = new TestViewModel(questions, studentId, FullName);
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, вывод сообщения об ошибке
                MessageBox.Show("Не удалось загрузить вопросы: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                // Не закрываем окно, оставляем его открытым
            }
        }
    }
}
