using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace KtoPierwszy2
{
    public class MainPageModel : INotifyPropertyChanged
    {
        private string selectedAnswers;

        public event PropertyChangedEventHandler PropertyChanged;

        private Stopwatch Timer = new Stopwatch();

        public string SelectedAnswers
        {
            get { return selectedAnswers; }
            set
            {
                selectedAnswers = value;
                OnPropertyChanged(nameof(SelectedAnswers)); // Notify that there was a change on this property
            }
        }

        ObservableCollection<string> answers = new ObservableCollection<string>();
        public ObservableCollection<string> Answers { get { return answers; } }

        public void StartTimer()
        {
            Timer.Start();
        }

        public void StopTimer()
        {
            Timer.Stop();
        }

        public TimeSpan GetElapsedTime()
        {
            return Timer.Elapsed;
        }

        public void ResetTimer()
        {
            Timer.Reset();
        }

        public bool IsTimerRunnning()
        {
            return Timer.IsRunning;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
