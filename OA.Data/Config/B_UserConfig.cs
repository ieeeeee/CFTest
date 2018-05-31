using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
    public class B_UserConfig:EntityTypeConfiguration<B_UserEntity>
    {

        public B_UserConfig()
        {
            ToTable("B_User");
            HasKey(item => item.UserID);
            Property(item => item.UserID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.UserNo).HasColumnType("varchar").HasMaxLength(6);
            Property(item => item.UserName).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            Property(item => item.NickName).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            Property(item => item.Password).HasColumnType("varchar").IsRequired().HasMaxLength(50);
            Property(item => item.PlainCode).HasColumnType("varchar").IsRequired().HasMaxLength(50);
            Property(item => item.Email).HasColumnType("nvarchar").HasMaxLength(20);
            Property(item => item.Tel).HasColumnType("nvarchar").HasMaxLength(20);
            Property(item => item.Address).HasColumnType("nvarchar").HasMaxLength(80);
            Property(item => item.Position).HasColumnType("nvarchar").HasMaxLength(20);
            Property(item => item.Gender).HasColumnType("varchar").HasMaxLength(2);

            HasOptional(x => x.B_Department).WithMany(x => x.B_Users).HasForeignKey(x => x.DeptID).WillCascadeOnDelete(false);
            HasOptional(x => x.B_Enterprise).WithMany(x => x.B_Users).HasForeignKey(x => x.EntID).WillCascadeOnDelete(false);
            //B_Role n:n

        }
    }
}