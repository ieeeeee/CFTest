using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
    public class P_LeaveApplyConfig:EntityTypeConfiguration<P_LeaveApplyEntity>
    {
        public P_LeaveApplyConfig()
        {
            ToTable("P_LeaveApply");
            HasKey(item => item.LeaveApplyID);
            Property(item => item.LeaveApplyID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.LeaveApplyTitle).HasColumnType("nvarchar").IsRequired().HasMaxLength(100);
            Property(item => item.LeaveApplyType).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            Property(item => item.StartTime).HasColumnType("DateTime");
            Property(item => item.EndTime).HasColumnType("DateTime");

            //P_ApplyOperator 和申请操作者是 0..1：n
            HasMany(b => b.P_ApplyOperators).WithOptional(a => a.P_LeaveApply);
        }
    }
}