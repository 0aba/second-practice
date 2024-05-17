using Microsoft.EntityFrameworkCore;


namespace Practice_2_6WithORM.DB;

public class ApplicationContext: DbContext
{
    public ApplicationContext()
    {
        Database.EnsureCreated();
    }
    
    public DbSet<UserAPP> Users { get; set; }
    public DbSet<TodoTask> TodoTasks { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new Configurations.UserAPPConfiguration());
        modelBuilder.ApplyConfiguration(new Configurations.TodoTaskConfiguration());
    } 
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=todo_db2;Username=admin_garage_db;Password=0admin12345");
    }
}