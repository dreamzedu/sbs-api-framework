using System;
using System.Collections.Generic;
using System.Text;
using sbs_api.models;

namespace sbs_api.repository
{
    public interface IQuestionRepo
    {
        List<QuestionGroup> GetGroupWiseQuestions();

        void InsertQuestion(Question q);

        void UpdateQuestion(int id, Question q);

        void DeleteQuestion(int id);

        List<PossibleAnswers> GetPossibleAnswers();

        IEnumerable<Question> GetQuestions();

        IEnumerable<Heading> GetQuestionHeadings();
    }
}
