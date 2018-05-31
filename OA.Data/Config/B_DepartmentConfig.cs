using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace OA.Data.Config
{
    public class B_DepartmentConfig:EntityTypeConfiguration<B_DepartmentEntity>
    {
        public B_DepartmentConfig()
        {
            ToTable("B_Department");
            HasKey(item => item.DepartmentID);
            Property(item => item.DepartmentID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(item => item.DeptNo).HasColumnType("varchar").HasMaxLength(6);
            Property(item => item.DeptName).HasColumnType("nvarchar").IsRequired().HasMaxLength(40);

            //B_User 1:n
           // HasMany(b => b.B_Users).WithOptional(a => a.B_Department).HasForeignKey(b => b.DepartmentID);
        }
    }
}