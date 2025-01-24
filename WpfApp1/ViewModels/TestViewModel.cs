using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.Helpers;
using WpfApp1.ViewModels;
using WpfApp1.Views;

public class TestViewModel : BaseViewModel
{
    private readonly ObservableCollection<Question> _questions;
    private int _currentIndex;
    private string _selectedAnswer;
    private int _correctAnswers;
    private readonly string _studentId;
    private readonly string _studentName;
    private readonly DateTime _testStartTime;

    public Question CurrentQuestion => _questions[_currentIndex];
    public string SelectedAnswer
    {
        get => _selectedAnswer;
        set => SetProperty(ref _selectedAnswer, value);
    }

    public ICommand NextQuestionCommand { get; }
    public ICommand FinishTestCommand { get; }

    public TestViewModel(ObservableCollection<Question> questions, string studentId, string studentName)
    {
        _questions = questions;
        _studentId = studentId;
        _studentName = studentName;
        _testStartTime = DateTime.Now;

        NextQuestionCommand = new RelayCommand(NextQuestion, CanMoveNext);
        FinishTestCommand = new RelayCommand(FinishTest);
    }

    private void NextQuestion()
    {
        SaveAnswer();
        _currentIndex++;

        if (_currentIndex < _questions.Count)
        {
            SelectedAnswer = null;
            OnPropertyChanged(nameof(CurrentQuestion));
        }
    }

    private void SaveAnswer()
    {
        if (SelectedAnswer == CurrentQuestion.CorrectAnswer)
        {
            _correctAnswers++;
        }
    }

    private bool CanMoveNext() => _currentIndex < _questions.Count - 1;

    private void FinishTest()
    {
        SaveAnswer();

        var totalQuestions = _questions.Count;
        var durationMinutes = (int)(DateTime.Now - _testStartTime).TotalMinutes;
        var grade = _correctAnswers >= totalQuestions * 0.8 ? "Отлично" :
                    _correctAnswers >= totalQuestions * 0.6 ? "Хорошо" : "Удовлетворительно";

        var result = new TestResult
        {
            StudentId = _studentId,
            StudentName = _studentName,
            TestDate = _testStartTime,
            TotalQuestions = totalQuestions,
            CorrectAnswers = _correctAnswers,
            Grade = grade,
            DurationMinutes = durationMinutes
        };

        ShowResults(result);
    }

    private void ShowResults(TestResult result)
    {
        var resultsWindow = new TestResultsWindow
        {
            DataContext = new TestResultsViewModel(new ObservableCollection<TestResult> { result })
        };
        resultsWindow.ShowDialog();
    }
}
