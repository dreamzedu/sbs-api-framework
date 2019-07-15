using System;
using System.Collections.Generic;
using System.Text;

namespace sbs_api.models
{
    public class QuestionGroup
    {
        public string text;

        public List<Question> questions = new List<Question>();
    }
}
