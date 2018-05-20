using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
    class C_CustomerConfig:EntityTypeConfiguration<C_CustomerEntity>
    {
        public C_CustomerConfig()
        {
            ToTable("C_Customer");
            HasKey(item => item.CustomerID);
            Property(item => item.CustomerID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.CustomerNO).HasColumnType("varchar").HasMaxLength(6);
            Property(item => item.CustomerName).HasColumnType("nvarchar").IsRequired().HasMaxLength(100);
            Property(item => item.Address).HasColumnType("nvarchar").HasMaxLength(100);
            Property(item => item.CustomerType).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);

            //C_Contacts 0..1：n
            HasMany(b => b.C_Contacts).WithOptional(a => a.C_Customer);

        }
    }
}