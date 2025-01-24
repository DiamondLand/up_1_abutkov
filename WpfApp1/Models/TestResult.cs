using System;

namespace WpfApp1.Models
{
    public class TestResult
    {
        public int TestId { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime TestDate { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public string Grade { get; set; }
        public int DurationMinutes { get; set; }
    }

}
