using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.ExamSubmissionModule
{
    public enum QuestionMarkedStatus
    {
        [EnumMember(Value = "Marked")]
        Marked,
        [EnumMember(Value = "Pending")]
        Pending
    }
}
