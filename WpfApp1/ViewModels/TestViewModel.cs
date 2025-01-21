using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using WpfApp1.Helpers;
using WpfApp1.Models;
using WpfApp1.ViewModels;
using WpfApp1.Views;

public class TestViewModel : BaseViewModel
{
    private readonly ObservableCollection<Question> _questions;
    private int _currentIndex;
    private int _correctAnswers;
    private string _selectedAnswer;
    private readonly string _studentId;
    private DateTime _testStartTime;

    public ObservableCollection<TestResult> TestResults { get; } = new ObservableCollection<TestResult>();

    public Question CurrentQuestion { get; private set; }
    public string SelectedAnswer
    {
        get => _selectedAnswer;
        set => SetProperty(ref _selectedAnswer, value);
    }

    public ICommand NextQuestionCommand { get; }
    public ICommand PreviousQuestionCommand { get; }
    public ICommand FinishTestCommand { get; }

    public TestViewModel(ObservableCollection<Question> questions, string studentId)
    {
        _questions = questions;
        _studentId = studentId;
        _testStartTime = DateTime.Now;

        CurrentQuestion = _questions[_currentIndex];
        NextQuestionCommand = new RelayCommand(NextQuestion, CanMoveNext);
        PreviousQuestionCommand = new RelayCommand(PreviousQuestion, CanMovePrevious);
        FinishTestCommand = new RelayCommand(FinishTest);
    }

    private void NextQuestion()
    {
        SaveAnswer(); // Сохраняем выбранный ответ
        _currentIndex++;

        if (_currentIndex < _questions.Count)
        {
            CurrentQuestion = _questions[_currentIndex];
            SelectedAnswer = null; // Сбрасываем выбранный ответ
            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(SelectedAnswer));
        }
    }

    private void PreviousQuestion()
    {
        _currentIndex--;
        if (_currentIndex >= 0)
        {
            CurrentQuestion = _questions[_currentIndex];
            OnPropertyChanged(nameof(CurrentQuestion));
        }
    }

    private bool CanMoveNext() => _currentIndex < _questions.Count - 1;
    private bool CanMovePrevious() => _currentIndex > 0;

    private void SaveAnswer()
    {
        if (CurrentQuestion.CorrectAnswer == SelectedAnswer)
        {
            _correctAnswers++;
        }
    }

    private void FinishTest()
    {
        SaveAnswer();

        int totalQuestions = _questions.Count;
        int duration = (int)(DateTime.Now - _testStartTime).TotalMinutes;
        string grade = _correctAnswers >= totalQuestions * 0.8 ? "Отлично" :
                       _correctAnswers >= totalQuestions * 0.6 ? "Хорошо" : "Удовлетворительно";

        foreach (var question in _questions)
        {
            TestResults.Add(new TestResult
            {
                TestId = 1,
                TestDate = _testStartTime,
                StudentId = _studentId,
                QuestionId = question.Id,
                Answer = SelectedAnswer,
                DurationMinutes = duration,
                TotalQuestions = totalQuestions,
                CorrectAnswers = _correctAnswers,
                Grade = grade
            });
        }

        ShowResults();
    }

    private void ShowResults()
    {
        var resultsWindow = new TestResultsWindow
        {
            DataContext = new TestResultsViewModel(TestResults)
        };
        resultsWindow.ShowDialog();
    }
}
