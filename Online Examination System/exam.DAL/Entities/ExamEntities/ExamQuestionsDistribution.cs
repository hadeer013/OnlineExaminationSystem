﻿using exam.DAL.Entities.QuestionsModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Entities.ExamEntities
{
    public class ExamQuestionsDistribution:BaseEntity
    {
        public int ChapterId { get; set; }
        [ForeignKey(nameof(ChapterId))]
        public Chapter? Chapter { get; set; }
        public QustionDifficulty qustionDifficulty { get; set; }
        public QuestionTypes QuestionType{ get; set; }
        public int NumOfQuestion{ get; set; }
        public int? ExamId { get; set; }
        [ForeignKey(nameof(ExamId))]
        public Exam? Exam { get; set; }
    }
}
