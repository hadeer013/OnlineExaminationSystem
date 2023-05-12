using exam.DAL.Entities;
using exam.DAL.Entities.ExamEntities;
using exam.DAL.Entities.ExamSubmissionModule;
using exam.DAL.Entities.ProfessorEntities;
using exam.DAL.Entities.QuestionsModule;
using exam.DAL.Entities.QuestionsModule.MCQModule;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Data
{
    public class ExaminationContext : IdentityDbContext<ApplicationUser>
    {
        public ExaminationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Level>? Levels { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<Subject>? Subjects { get; set; }
        public DbSet<Admin>? Admins  { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<Professor>? Professors { get; set; }
        public DbSet<ProfessorSignUpRequests>? professorSignUpRequests { get; set; }
        public DbSet<Chapter>? Chapters { get; set; }
        public DbSet<Question>? Questions { get; set; }
        public DbSet<MCQwithMultipleCorrectAns>? MCQwithMultipleCorrectAns { get; set; }
        public DbSet<MCQwithOneCorrectAns>? MCQwithOneCorrectAns { get; set; }
        public DbSet<OpenEndedQuestion>? OpenEndedQuestion { get; set; }
        public DbSet<TrueFalseQuestion>? TrueFalseQuestion { get; set; }

        public DbSet<QuestionTypePoint> QuestionTypePoints { get; set; }
        public DbSet<ExamQuestionsDistribution> ExamQuestionsDistributions { get; set; }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<StudentExamCopy> StudentExamCopy { get; set; }
        public DbSet<StudentExamCopyWithQuestionRelationhip> ExamCopyWithQuestionRelationhips { get; set; }
        public DbSet<QuestionSubmission> QuestionSubmissions { get; set; }
        public DbSet<ExamSubmission> ExamSubmissions { get; set; }
        public DbSet<Exam> Exams { get; set; }
    }
}
