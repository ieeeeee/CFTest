using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
    public class B_LogConfig:EntityTypeConfiguration<B_LogEntity>
    {
        public B_LogConfig()
        {
            ToTable("B_log");
            HasKey(item => item.LogID);
            Property(item => item.LogID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(item => item.IP).HasColumnType("varchar").HasMaxLength(20);
            Property(item => item.Mac).HasColumnType("nvarchar").HasMaxLength(40);
            Property(item => item.LogType).IsRequired();
            Property(item => item.UserID).IsRequired();
            Property(item => item.MenuID).IsRequired();
            Property(item => item.UserName).HasColumnType("varchar").HasMaxLength(20);
            Property(item => item.MenuName).HasColumnType("varchar").HasMaxLength(40);
        }
    }
}