using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
    public class B_MenuConfig:EntityTypeConfiguration<B_MenuEntity>
    {
        public B_MenuConfig()
        {
            ToTable("B_Menu");
            HasKey(item => item.MenuID);
            Property(item => item.MenuID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.MenuName).HasColumnType("nvarchar").IsRequired().HasMaxLength(60);
            Property(item => item.MenuType).IsRequired();
            Property(item => item.Url).HasColumnType("nvarchar").HasMaxLength(60);
            Property(item => item.ParentID).IsRequired();
            Property(item => item.OrderID).IsRequired();

            //B_Roles  n:n

            HasMany(b => b.B_Roles).WithMany(b=>b.B_Menus).Map(c=>
            {
                    c.ToTable("B_MenuRole");
                    c.MapLeftKey("MenuID");
                    c.MapRightKey("RoleID");
            });
        }
    }
}