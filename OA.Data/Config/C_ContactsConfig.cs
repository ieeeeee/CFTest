using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
    public class C_ContactsConfig:EntityTypeConfiguration<C_ContactsEntity>
    {
        public C_ContactsConfig()
        {
            ToTable("C_Contacts");
            HasKey(item => item.ContactsID);
            Property(item => item.ContactsID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.ContactsName).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            Property(item => item.Tel1).HasColumnType("varchar").HasMaxLength(20);
            Property(item => item.Tel2).HasColumnType("varchar").HasMaxLength(20);
            Property(item => item.Tel3).HasColumnType("varchar").HasMaxLength(20);
        }
    }
}