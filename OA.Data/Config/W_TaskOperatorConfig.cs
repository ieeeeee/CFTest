using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
   public class W_TaskOperatorConfig:EntityTypeConfiguration<W_TaskOperatorEntity>
    {
        public W_TaskOperatorConfig()
        {
            ToTable("W_TaskOperator");
            HasKey(item => item.TaskOperatID);
            Property(item => item.TaskOperatID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.TaskOperatType).IsRequired();
        }
    }
}