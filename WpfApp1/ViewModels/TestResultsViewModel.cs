using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Helpers;
using WpfApp1.Models;
using System.Linq;

namespace WpfApp1.ViewModels
{
    public class TestResultsViewModel : BaseViewModel
    {
        public ObservableCollection<TestResult> TestResults { get; set; }
        public ICommand CloseCommand { get; }

        public TestResultsViewModel(ObservableCollection<TestResult> results)
        {
            TestResults = results;
            CloseCommand = new RelayCommand(OnClose);
        }

        private void OnClose()
        {
            var window = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.DataContext == this);
            window?.Close();
        }
    }
}
