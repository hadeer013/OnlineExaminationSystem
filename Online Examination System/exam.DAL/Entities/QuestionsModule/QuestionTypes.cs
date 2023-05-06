using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.QuestionsModule
{
    public enum QuestionTypes
    {
        [EnumMember(Value = "MCQ")]
        MCQ,
        [EnumMember(Value = "TrueAndFalse")]
        TrueAndFalse,
        [EnumMember(Value = "OpenEnded")]
        OpenEnded
    }
}
