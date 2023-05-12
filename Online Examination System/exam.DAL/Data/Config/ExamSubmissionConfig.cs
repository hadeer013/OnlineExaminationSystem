using exam.DAL.Entities.ExamSubmissionModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam.DAL.Data.Config
{
    public class ExamSubmissionConfig : IEntityTypeConfiguration<ExamSubmission>
    {
        public void Configure(EntityTypeBuilder<ExamSubmission> builder)
        {
            builder.HasKey(e=>e.ExamCopyId);
        }
    }
}
