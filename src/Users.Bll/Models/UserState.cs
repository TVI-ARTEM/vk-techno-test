using System.ComponentModel.DataAnnotations.Schema;

namespace Users.Bll.Models;

[Table("user_states")]
public record UserState
{
    [Column("id")] public long Id { get; set; }

    [Column("code", TypeName = "nvarchar(10)")]
    public UserStateEnum Code { get; set; }

    [Column("description")] public string Description { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}