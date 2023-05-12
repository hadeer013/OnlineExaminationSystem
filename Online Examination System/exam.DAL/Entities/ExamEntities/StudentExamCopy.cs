using exam.DAL.Entities.QuestionsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.ExamEntities
{
    public class StudentExamCopy:BaseEntity
    {
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public string StudentId { get; set;}
        public Student Student { get; set; }
        public DateTime TakeExamTime {  get; set; }
        public int NumOfSubmittion { get; set; } = 0;
        public IEnumerable<Question> Questions { get; set; }
    }
}
