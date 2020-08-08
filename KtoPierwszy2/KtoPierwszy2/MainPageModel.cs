using KtoPierwszy2.Questions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace KtoPierwszy2
{
  internal class MainPageModel : INotifyPropertyChanged
  {        
    private string selectedAnswers;
    private IQuestionProvider m_questionProvider;
    private Stopwatch Timer = new Stopwatch();
    private Question m_currentQuestion;
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
      m_currentQuestion = m_questionProvider.GetNextQuestion();
      Question = m_currentQuestion.Body;
      TextAnswers = new List<string>() { string.Empty, string.Empty, string.Empty, string.Empty };

      OnPropertyChanged(nameof(Question));
      OnPropertyChanged(nameof(TextAnswers));

      Task.Run(() => {
        System.Threading.Thread.Sleep(5000);
        TextAnswers = m_currentQuestion.GetAnswersInRandomOrder().GetAnswers();
        StartTimer();
        OnPropertyChanged(nameof(TextAnswers));
      });
    }

    public List<string> TextAnswers { get; set; }
    public string Question { get; set; }

    ObservableCollection<string> answers = new ObservableCollection<string>();
    public ObservableCollection<string> Answers { get { return answers; } }

    public bool AreAnswersCorrect(Answers selectedAnswers)
    {
      return m_currentQuestion.AreAnswersCorrect(selectedAnswers);
    }

    public Answers GetCorrectAnswers()
    {
      return m_currentQuestion.GetAnswersInCorrectOrder();
    }

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
