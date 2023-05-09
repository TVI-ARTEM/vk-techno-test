using System.ComponentModel.DataAnnotations.Schema;

namespace Users.Bll.Models;

[Table("user_groups")]
public record UserGroup
{
    [Column("id")] public long Id { get; set; }

    [Column("code", TypeName = "nvarchar(24)")]
    public UserGroupEnum Code { get; set; }

    [Column("description")] public string Description { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}