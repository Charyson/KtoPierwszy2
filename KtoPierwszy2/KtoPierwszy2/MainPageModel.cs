using KtoPierwszy2.Questions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Timers;

namespace KtoPierwszy2
{
  internal class MainPageModel : INotifyPropertyChanged
  {        
    private string selectedAnswers;
    private IQuestionProvider m_questionProvider;
    private Stopwatch Timer = new Stopwatch();
    public event PropertyChangedEventHandler PropertyChanged;

    public MainPageModel(IQuestionProvider questionProvider)
    {
      m_questionProvider = questionProvider;
    }

    public string SelectedAnswers
    {
        get { return selectedAnswers; }
        set
        {
            selectedAnswers = value;
            OnPropertyChanged(nameof(SelectedAnswers)); // Notify that there was a change on this property
        }
    }

    public void GetNextQuestion()
    {
      var question = m_questionProvider.GetNextQuestion();
      Question = question.Body;
      TextAnswers = question.GetAnswersInRandomOrder().GetAnswers();

      OnPropertyChanged(nameof(Question));
      OnPropertyChanged(nameof(TextAnswers));
    }

    public List<string> TextAnswers { get; set; }
    public string Question { get; set; }

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
