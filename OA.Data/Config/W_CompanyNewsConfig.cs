using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
    public class W_CompanyNewsConfig:EntityTypeConfiguration<W_CompanyNewsEntity>
    {
        public W_CompanyNewsConfig()
        {
            ToTable("W_CompanyNews");
            HasKey(item => item.NewsID);
            Property(item => item.NewsID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.NewsTitle).HasColumnType("nvarchar").IsRequired().HasMaxLength(60);
            Property(item => item.NewsBody).HasColumnType("nvarchar").IsRequired();
            Property(item => item.NewsType).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
        }
    }
}