using System.Linq;

namespace KtoPierwszy2.Questions
{
  internal class Question
  {
    public string Body { get; }
    private Answers m_answersInCorrectOrder;

    public Question(string question, Answers answersInCorrectOrder)
    {
      Body = question;
      m_answersInCorrectOrder = answersInCorrectOrder;
    }

    public Answers GetAnswersInCorrectOrder()
    {
      return m_answersInCorrectOrder;
    }

    public Answers GetAnswersInRandomOrder()
    {
      var answersInRandomOrder = m_answersInCorrectOrder.ToList();
      answersInRandomOrder.Shuffle();

      return Answers.CreateFromList(answersInRandomOrder);
    }

    public bool AreAnswersCorrect(Answers proposedAnswers)
    {
      return m_answersInCorrectOrder.GetAnswers().SequenceEqual(proposedAnswers.GetAnswers());
    }
  }
}
