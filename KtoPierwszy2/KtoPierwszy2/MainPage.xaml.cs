using KtoPierwszy2.DI;
using KtoPierwszy2.Questions;
using Ninject;
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

    public MainPage()
    {
      InitializeComponent();
      m_kernel = new StandardKernel(new AppModule());
      m_model = new MainPageModel(m_kernel.Get<IQuestionProvider>());
      m_model.GetNextQuestion();
      this.BindingContext = m_model;
    }

    void AcceptButton_Clicked(object sender, System.EventArgs e)
    {
        MyAnswers.SelectedItem = null;
        m_model.StopTimer();
        Time.Text = "Czas: " + m_model.GetElapsedTime().ToString(@"ss\.fff") + " s";
        m_model.ResetTimer();
        this.AcceptButton.IsEnabled = false;
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
      m_model.SelectedAnswers = "Brak odpowiedzi";
      Time.Text = string.Empty;
      m_model.ResetTimer();
      this.AcceptButton.IsEnabled = false;
      m_model.GetNextQuestion();
    }

    private void MyAnswers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedAnswer = e.SelectedItem as String;
        if (!m_model.Answers.Contains(selectedAnswer))
        {
            m_model.Answers.Add(selectedAnswer);
        }
        m_model.SelectedAnswers = "Wybrane odpowiedzi: " + string.Join(" ", m_model.Answers);
        if (m_model.Answers.Count == 4)
        {
            this.AcceptButton.IsEnabled = true;
        }
    }
  }
}
