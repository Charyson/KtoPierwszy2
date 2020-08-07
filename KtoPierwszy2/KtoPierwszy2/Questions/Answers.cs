using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KtoPierwszy2.Questions
{
  internal class Answers : IEnumerable<string>
  {
    private List<string> m_answers;

    public Answers(string first, string second, string third, string fourth)
    {
      m_answers = new List<string>()
      {
        first.Trim(), second.Trim(), third.Trim(), fourth.Trim()
      };
    }

    public static Answers CreateFromList(IList<string> answers)
    {
      if (answers == null)
      {
        throw new ArgumentNullException("Cannot create answers from null list");
      }

      if (answers.Count != 4)
      {
        throw new ArgumentException($"Expected the number of answers to be 4, but was {answers.Count}");
      }

      return new Answers(answers[0], answers[1], answers[2], answers[3]);
    }

    public List<string> GetAnswers()
    {
      return m_answers;
    }

    public IEnumerator<string> GetEnumerator()
    {
      return ((IEnumerable<string>)m_answers).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable)m_answers).GetEnumerator();
    }
  }
}
