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
    private readonly string _studentName;
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

    public TestViewModel(ObservableCollection<Question> questions, string studentId, string studentName)
    {
        _questions = questions;
        _studentId = studentId;
        _studentName = studentName;
        _testStartTime = DateTime.Now;

        CurrentQuestion = _questions[_currentIndex];
        NextQuestionCommand = new RelayCommand(NextQuestion, CanMoveNext);
        PreviousQuestionCommand = new RelayCommand(PreviousQuestion, CanMovePrevious);
        FinishTestCommand = new RelayCommand(FinishTest);
    }

    private void NextQuestion()
    {
        SaveAnswer();
        _currentIndex++;

        if (_currentIndex < _questions.Count)
        {
            CurrentQuestion = _questions[_currentIndex];
            SelectedAnswer = null;
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
        if (!string.IsNullOrEmpty(SelectedAnswer))
        {
            if (SelectedAnswer == CurrentQuestion.CorrectAnswer)
            {
                _correctAnswers++;
            }
        }
    }


    private void FinishTest()
    {
        SaveAnswer();

        int totalQuestions = _questions.Count;
        int duration = (int)(DateTime.Now - _testStartTime).TotalMinutes;
        string grade = _correctAnswers >= totalQuestions * 0.8 ? "Отлично" :
                       _correctAnswers >= totalQuestions * 0.6 ? "Хорошо" : "Удовлетворительно";

        var testResult = new TestResult
        {
            TestId = 1,
            StudentId = _studentId,
            StudentName = _studentName,
            TestDate = _testStartTime,
            TotalQuestions = totalQuestions,
            CorrectAnswers = _correctAnswers,
            Grade = grade,
            DurationMinutes = duration
        };

        TestResults.Add(testResult);

        ShowResults(testResult);
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
