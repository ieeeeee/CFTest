using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace OA.Data.Config
{
    public  class B_BaseClassConfig:EntityTypeConfiguration<B_BaseClassEntity>
    {
        public B_BaseClassConfig()
        {
            ToTable("B_BaseClass");
            HasKey(item => item.BaseClassID);
            Property(item => item.BaseClassID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(item => item.BaseClassName).HasColumnType("nvarchar").IsRequired().HasMaxLength(100);

            //B_BaseInfo 1:n 指定外键 并设置级联删除
            HasMany(B => B.B_BaseInfos).WithRequired(A => A.B_BaseClass).HasForeignKey(B => B.BaseClassID).WillCascadeOnDelete();

            
        }
    }
}
//在一对多的关系里 在1的关系配置类里写  指定外键  有必要级联删除；在多的实体类里指定1的主键
//在多对多的关系里 随意在一个关系配置里写 并要生成一个表 