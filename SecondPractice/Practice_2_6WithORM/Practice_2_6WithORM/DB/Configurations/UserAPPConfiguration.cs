using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Practice_2_6WithORM.DB.Configurations;

public class UserAPPConfiguration: IEntityTypeConfiguration<UserAPP>
{
    public void Configure(EntityTypeBuilder<UserAPP> builder)
    {
        builder
            .HasMany(user => user.UserTasks)
            .WithOne(task => task.User)
            .HasForeignKey(t => t.UserPk)
            .IsRequired(true);
        
        builder
            .ToTable(userTable => userTable.HasCheckConstraint("CK_login", "LENGTH(login) >= 8"));
        
        builder
            .ToTable(userTable => userTable.HasCheckConstraint("CK_pass", "LENGTH(pass) >= 8"));
    }
}