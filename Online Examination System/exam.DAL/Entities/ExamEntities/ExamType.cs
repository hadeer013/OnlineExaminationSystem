using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.ExamEntities
{
    public enum ExamType
    {
        [EnumMember(Value = "Final")]
        Final,
        [EnumMember(Value = "MidTerm")]
        MidTerm,
        [EnumMember(Value = "Test")]
        Test,
        [EnumMember(Value = "Quiz")]
        Quiz
    }
}
