using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
    public  class B_BaseInfoConfig:EntityTypeConfiguration<B_BaseInfoEntity> //允许为模型中的实体类型进行配置
    {
        public B_BaseInfoConfig()
        {
            ToTable("B_BaseInfo");
            HasKey(item => item.BaseInfoID);
            Property(item => item.BaseInfoID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(item => item.BaseName).HasColumnType("nvarchar").IsRequired().HasMaxLength(100);
            Property(item => item.BaseValue).HasColumnType("nvarchar").IsRequired().HasMaxLength(80);
            Property(item => item.BaseClassID).IsRequired();
        }
    }
}