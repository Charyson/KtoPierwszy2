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
      var answers = question.GetAnswersInRandomOrder().GetAnswers();
      AnswerA = answers[0];
      AnswerB = answers[1];
      AnswerC = answers[2];
      AnswerD = answers[3];

      OnPropertyChanged(nameof(Question));
      OnPropertyChanged(nameof(AnswerA));
      OnPropertyChanged(nameof(AnswerB));
      OnPropertyChanged(nameof(AnswerC));
      OnPropertyChanged(nameof(AnswerD));
    }

    public string AnswerA { get; set; }
    public string AnswerB { get; set; }
    public string AnswerC { get; set; }
    public string AnswerD { get; set; }
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
