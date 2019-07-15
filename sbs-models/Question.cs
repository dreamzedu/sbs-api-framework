using System;
using System.Collections.Generic;

namespace sbs_api.models
{
    public class Question
    {
        public int id;
        public string text;
        public string possible_answers;
        public int? headingId=null;
        public int index;
        public int isactive;
    }
}
