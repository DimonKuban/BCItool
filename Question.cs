using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BCItester
{
    public class Question
    {
        public String QuestionText { get; }
        public QuestionType.QType QType { get; }
        public String CorrectAnswer { get; }
        public int index { get; }

        public Question(QuestionType.QType qType, String qText, String cAnswer, int index)
        {
            this.index = index;
            this.CorrectAnswer = cAnswer;
            this.QType = qType;
            this.QuestionText = qText;
        }
    }

}
