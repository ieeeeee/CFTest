using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
    public class S_SystemConfig:EntityTypeConfiguration<S_SystemConfigEntity>
    {
        public S_SystemConfig()
        {
            ToTable("S_SystemConfig");
            HasKey(item => item.SystemConfigID);
            //Property(item => item.SystemConfigID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.SystemName).HasColumnType("varchar").IsRequired().HasMaxLength(20);
            Property(item => item.IsDataInited).IsRequired();
            Property(item => item.DataInitedDate).IsRequired();
        }
    }
}
