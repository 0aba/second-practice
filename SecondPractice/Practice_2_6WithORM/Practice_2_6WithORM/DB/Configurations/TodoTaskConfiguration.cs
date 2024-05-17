using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Practice_2_6WithORM.DB.Configurations;

public class TodoTaskConfiguration: IEntityTypeConfiguration<TodoTask>
{
    public void Configure(EntityTypeBuilder<TodoTask> builder)
    {
        builder
            .Property(task => task.Complited)
            .HasDefaultValue(false);
    }
}