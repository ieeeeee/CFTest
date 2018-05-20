using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
    public class W_MeetingReportConfig:EntityTypeConfiguration<W_MeetingReportEntity>
    {
        public W_MeetingReportConfig()
        {

            ToTable("W_MeetingReport");
            HasKey(item => item.MeetingID);
            Property(item => item.MeetingID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.MeetingTitle).HasColumnType("nvarchar").IsRequired().HasMaxLength(60);
            Property(item => item.MeetingBody).HasColumnType("nvarchar").IsRequired();
            Property(item => item.MeetingType).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
        }
    }
}