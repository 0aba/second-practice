using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Practice_2_6WithORM.DB;

[Table("todo_task")]
public class TodoTask
{
    [Key, Column("pk", TypeName = "INT")]
    public int Pk { get; set; }
    [Required, Column("pk_user", TypeName = "INT"), ForeignKey("user_app")]
    public int UserPk { get; set; }
    public UserAPP User { get; set; } = null!;
    [Required, Column("title", TypeName = "VARCHAR(64)")]
    public string Title { get; set; } = string.Empty;
    [Required, Column("text_todo", TypeName = "VARCHAR(128)")]
    public string TextTodo { get; set; } = string.Empty;
    [Required, Column("time_end", TypeName = "TIMESTAMP"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode=true)]
    public DateTime TimeEnd { get; set; }
    // не работает анатация DefaultValue(false), оставил для вида
    [Required, DefaultValue(false), Column("complited", TypeName = "BOOLEAN")]
    public bool Complited { get; set; } = false;
}