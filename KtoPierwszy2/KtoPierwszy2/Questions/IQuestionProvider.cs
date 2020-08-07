using System;
using System.Collections.Generic;
using System.Text;

namespace KtoPierwszy2.Questions
{
  interface IQuestionProvider
  {
    Question GetNextQuestion();
  }
}
