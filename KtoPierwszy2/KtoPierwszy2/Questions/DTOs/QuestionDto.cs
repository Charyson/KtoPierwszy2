using KtoPierwszy2.Questions.DTOs;

namespace KtoPierwszy2.Questions
{
  internal class QuestionDto
  {
    public string Question { get; set; }
    public AnswersInCorrectOrderDto AnswersInCorrectOrder { get; set; }
  }
}