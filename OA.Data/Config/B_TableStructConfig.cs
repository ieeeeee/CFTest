using OA.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Data.Config
{
   public class B_TableStructConfig:EntityTypeConfiguration<B_TableStructEntity>
    {
        public B_TableStructConfig()
        {
            ToTable("B_TableStruct");
            HasKey(item => item.TStructID);
            Property(item => item.TStructID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(item => item.TableID).IsRequired();
            Property(item => item.TableName).IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(item => item.TableDescription).HasColumnType("nvarchar").HasMaxLength(100);
            Property(item => item.Field).IsRequired().HasColumnType("nvarchar").HasMaxLength(40);
            Property(item => item.FieldName).HasColumnType("nvarchar").HasMaxLength(100);
            Property(item => item.FieldDescription).HasColumnType("nvarchar").HasMaxLength(100);
            Property(item => item.ShowID).HasColumnType("nvarchar").HasMaxLength(40);
            Property(item => item.VueTemplate).HasColumnType("nvarchar").HasMaxLength(100);
            Property(item => item.Placeholder).HasColumnType("nvarchar").HasMaxLength(100);
            Property(item => item.HelpBlock).HasColumnType("nvarchar").HasMaxLength(100);
            Property(item => item.FieldLangName).HasColumnType("nvarchar").HasMaxLength(40);
        }
    }
}
