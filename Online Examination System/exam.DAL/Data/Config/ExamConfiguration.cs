using exam.DAL.Entities.ExamEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Data.Config
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasMany(e => e.examQuestionsDistributions)
                   .WithOne(d => d.Exam)
                   .HasForeignKey(d => d.ExamId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
