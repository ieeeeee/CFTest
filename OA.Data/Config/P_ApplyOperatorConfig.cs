using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
   public class P_ApplyOperatorConfig:EntityTypeConfiguration<P_ApplyOperatorEntity>
    {
        public P_ApplyOperatorConfig()
        {
            ToTable("P_ApplyOperator");
            HasKey(item => item.ApplyOperatID);
            Property(item => item.ApplyOperatID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.ApplyOperatType).HasColumnType("varchar").IsRequired().HasMaxLength(20);

        }
    }
}