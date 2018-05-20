using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
   public  class W_PlanListConfig:EntityTypeConfiguration<W_PlanListEntity>
    {
        public W_PlanListConfig()
        {
            ToTable("W_PlanList");
            HasKey(item => item.PlanID);
            Property(item => item.PlanID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.PlanTitle).HasColumnType("nvarchar").IsRequired().HasMaxLength(60);
            Property(item => item.PlanBody).HasColumnType("nvarchar").IsRequired();
            Property(item => item.PlanType).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
        }
    }
}