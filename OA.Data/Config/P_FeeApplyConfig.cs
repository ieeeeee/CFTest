using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
    public class P_FeeApplyConfig:EntityTypeConfiguration<P_FeeApplyEntity>
    {
        public P_FeeApplyConfig()
        {

            ToTable("P_FeeApply");
            HasKey(item => item.FeeApplyID);
            Property(item => item.FeeApplyID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.FeeApplyTitle).HasColumnType("nvarchar").IsRequired().HasMaxLength(100);
            Property(item => item.FeeApplyType).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            Property(item => item.FeeApplyAmount).HasColumnType("decimal").IsRequired().HasPrecision(14, 2);
            Property(item => item.FeeApproveAmount).HasColumnType("decimal").HasPrecision(14, 2);

            //P_ApplyOperator 和申请操作者是 0..1：n
            HasMany(b => b.P_ApplyOperators).WithOptional(a => a.P_FeeApply);
        }
    }
}