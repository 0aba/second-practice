using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Practice_2_6WithORM.DB;

[Table("user_app"), Index(nameof(Login), IsUnique = true, Name = "IX_unique_login")]
public class UserAPP
{
        [Key, Column("pk", TypeName = "INT")]
        public int Pk { get; set; }
        [Required, Column("login", TypeName = "VARCHAR(64)")]
        public string Login { get; set; } = string.Empty;
        [Required, Column("pass", TypeName = "VARCHAR(60)")]
        public string Pass { get; set; } = string.Empty;
        public virtual ICollection<TodoTask> UserTasks { get; set; } = [];
}