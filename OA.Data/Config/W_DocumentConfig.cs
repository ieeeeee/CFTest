using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
    public class W_DocumentConfig:EntityTypeConfiguration<W_DocumentEntity>
    {
        public W_DocumentConfig()
        {
            ToTable("W_Document");
            HasKey(item => item.DocID);
            Property(item => item.DocID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.DocName).HasColumnType("nvarchar").IsRequired().HasMaxLength(60);
            Property(item => item.DocUrlPath).HasColumnType("nvarchar").IsRequired().HasMaxLength(80);
            Property(item => item.DocType).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
        }
    }
}