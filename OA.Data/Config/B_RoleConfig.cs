using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
    public class B_RoleConfig:EntityTypeConfiguration<B_RoleEntity>
    {
        public B_RoleConfig()
        {
            ToTable("B_Role");
            HasKey(item => item.RoleID);
            Property(item => item.RoleID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.RoleName).HasColumnType("nvarchar").IsRequired().HasMaxLength(40);
            Property(item => item.Description).HasColumnType("nvarchar").HasMaxLength(50);

            HasMany(b => b.B_Users).WithMany(b => b.B_Roles).Map(c => {
                c.ToTable("B_UserRole");
                c.MapLeftKey("RoleID");
                c.MapRightKey("UserID");
            });
        }
    }
}