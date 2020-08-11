using KtoPierwszy2.DI;
using KtoPierwszy2.Questions;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KtoPierwszy2
{
  [DesignTimeVisible(false)]
  public partial class MainPage : ContentPage
  {
    private MainPageModel m_model;
    private readonly StandardKernel m_kernel;

    public MainPage(NinjectModule androidDependenciesModule)
    {
      InitializeComponent();
      m_kernel = new StandardKernel(new AppModule(), androidDependenciesModule);
      m_model = new MainPageModel(m_kernel.Get<IQuestionProvider>());
      m_model.GetNextQuestion();
      this.BindingContext = m_model;
    }

    async void AcceptButton_Clicked(object sender, System.EventArgs e)
    {
      MyAnswers.SelectedItem = null;
      m_model.StopTimer();
      Time.Text = "Czas: " + GetElapsedTimeString();
      this.AcceptButton.IsEnabled = false;
      this.CancelButton.IsEnabled = false;
      this.RestartButton.IsEnabled = true;
      await CheckAnswersAsync();
    }

    private string GetElapsedTimeString()
    {
      return m_model.GetElapsedTime().ToString(@"ss\.fff") + " s";
    }

    public void CancelButton_Clicked(object sender, System.EventArgs e)
    {
        m_model.Answers.Clear();
        MyAnswers.SelectedItem = null;
        m_model.SelectedAnswers = "Brak odpowiedzi";
        this.AcceptButton.IsEnabled = false;
    }

    public void RestartButton_Clicked(object sender, System.EventArgs e)
    {
      m_model.Answers.Clear();
      MyAnswers.SelectedItem = null;
      m_model.SelectedAnswers = string.Empty;
      Time.Text = string.Empty;
      m_model.ResetTimer();
      this.AcceptButton.IsEnabled = false;
      this.CancelButton.IsEnabled = true;
      this.RestartButton.IsEnabled = false;
      m_model.GetNextQuestion();
    }

    private async Task CheckAnswersAsync()
    {
      if (m_model.AreAnswersCorrect(Answers.CreateFromList(m_model.Answers.ToList())))
      {
        await DisplaySummaryPrompt("Dobrze!", "Oby tak dalej");
      }
      else
      {
        await DisplaySummaryPrompt("Niestety źle...", $"Poprawna kolejność to: {Environment.NewLine}{m_model.GetCorrectAnswers()}");
      }
    }

    private async Task DisplaySummaryPrompt(string title, string body)
    {
      await DisplayAlert(title, body + Environment.NewLine + $"Czas: {GetElapsedTimeString()}", "OK");
    }

    private void MyAnswers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedAnswer = e.SelectedItem as String;
        if (!string.IsNullOrWhiteSpace(selectedAnswer) && !m_model.Answers.Contains(selectedAnswer))
        {
            m_model.Answers.Add(selectedAnswer);
        }
        m_model.SelectedAnswers = "Wybrane odpowiedzi: " + string.Join("; ", m_model.Answers);
        if (m_model.Answers.Count == 4)
        {
            this.AcceptButton.IsEnabled = true;
        }
    }
  }
}
