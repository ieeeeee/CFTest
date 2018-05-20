using OA.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace OA.Data.Config
{
   public class W_TaskListConfig:EntityTypeConfiguration<W_TaskListEntity>
    {
        public W_TaskListConfig()
        {
            ToTable("W_TaskList");
            HasKey(item => item.TaskID);
            Property(item => item.TaskID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(item => item.TaskTitle).HasColumnType("nvarchar").IsRequired().HasMaxLength(60);
            Property(item => item.TaskBody).HasColumnType("nvarchar").IsRequired();
            Property(item => item.TaskType).HasColumnType("nvarchar").IsRequired().HasMaxLength(20);
            Property(item => item.TaskStatus).IsRequired();

            //W_TaskOperator 和任务操作者是 0..1：n
            HasMany(b => b.W_TaskOperators).WithOptional(a => a.W_Task);
        }
    }
}