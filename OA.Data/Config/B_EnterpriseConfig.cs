using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
    public class B_EnterpriseConfig:EntityTypeConfiguration<B_EnterpriseEntity>
    {
        public B_EnterpriseConfig()
        {
            ToTable("B_Enterprise");
            HasKey(item => item.EntID);
            Property(item => item.EntID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(item => item.EntName).HasColumnType("nvarchar").IsRequired().HasMaxLength(100);
            Property(item => item.Tel).HasColumnType("nvarchar").HasMaxLength(20);
            Property(item => item.Address).HasColumnType("nvarchar").HasMaxLength(100);

        }
    }
}