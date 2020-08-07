using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KtoPierwszy2.Questions
{
  class QuestionsFromJsonProvider : IQuestionProvider
  {
    private bool m_isInitialized = false;
    private static Random random = new Random();
    private Question[] m_questions;

    public Question GetNextQuestion()
    {
      if (!m_isInitialized)
      {
        Initialize();
      }

      return GetRandomQuestion();
    }

    private void Initialize()
    {
      if (!m_isInitialized)
      {
        var rawQuestions = LoadQuestionsFromFile();
        var questionsList = new List<Question>();
        foreach (var questionDto in rawQuestions)
        {
          questionsList.Add(new Question(questionDto.Question, new Answers(questionDto.AnswersInCorrectOrder.First,
            questionDto.AnswersInCorrectOrder.Second,
            questionDto.AnswersInCorrectOrder.Third,
            questionDto.AnswersInCorrectOrder.Fourth)));
        }

        m_questions = questionsList.ToArray();

        m_isInitialized = true;
      }
    }

    private Question GetRandomQuestion()
    {
      var randomIndex = random.Next(0, m_questions.Length);
      return m_questions[randomIndex];
    }

    private List<QuestionDto> LoadQuestionsFromFile()
    {
      var assembly = Assembly.GetExecutingAssembly();
      Stream stream = assembly.GetManifestResourceStream("KtoPierwszy2.Questions.json");

      string text;
      using (var reader = new System.IO.StreamReader(stream, Encoding.GetEncoding("Windows-1250")))
      {
        text = reader.ReadToEnd();
      }

      return JsonConvert.DeserializeObject<List<QuestionDto>>(text);
    }
  }
}
