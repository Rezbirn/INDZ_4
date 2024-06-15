using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace INDZ_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer _timer;
        private DateTime _startTime;

        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += Timer_Tick;
        }
        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                StartStopButton.Content = "Старт";

                TimeSpan elapsed = DateTime.Now - _startTime;
                string comment = (string)CommentsComboBox.Text;
                ResultsListBox.Items.Add($"{elapsed.Minutes}:{elapsed.Seconds}.{elapsed.Milliseconds / 100} - {comment}");

                StopwatchLabel.Content = "0:0";
            }
            else
            {
                _startTime = DateTime.Now;
                _timer.Start();
                StartStopButton.Content = "Стоп";
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - _startTime;
            StopwatchLabel.Content = $"{elapsed.Minutes}:{elapsed.Seconds}.{elapsed.Milliseconds / 100}";
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ResultsListBox.Items.Clear();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void CommentsComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string newComment = CommentsComboBox.Text;
                if (!string.IsNullOrWhiteSpace(newComment) && !IsCommentExist(newComment))
                {
                    CommentsComboBox.Items.Add(newComment);
                    CommentsComboBox.SelectedItem = newComment;
                }
            }
        }
        private bool IsCommentExist(string comment)
        {
            foreach (var item in CommentsComboBox.Items)
            {
                if (item.ToString() == comment)
                {
                    return true;
                }
            }
            return false;
        }
    }
}