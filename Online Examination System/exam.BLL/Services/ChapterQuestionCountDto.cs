using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.BLL.Services
{
    public class ChapterQuestionCountDto
    {
        public int ChapterId { get; set; }
        public string ChapterName { get; set; }
        public int QuestionCount { get; set; }
    }
}
