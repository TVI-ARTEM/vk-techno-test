using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Users.Bll.Models;

[Table("users")]
[Index(nameof(Login), IsUnique = true, Name = "IX_users_login")]
public record User
{
    [Column("id")] public long Id { get; set; }

    [Column("login")] public string Login { get; set; } = null!;
    [Column("password")] public string Password { get; set; } = null!;
    [Column("created_date")] public DateTime CreatedDate { get; set; }

    [Column("user_group_id")] public long UserGroupId { get; set; }

    [Column("user_state_id")] public long UserStateId { get; set; }

    /*[ForeignKey("user_group_id")]*/ public virtual UserGroup UserGroup { get; set; } = null!;
    /*[ForeignKey("user_state_id")]*/ public virtual UserState UserState { get; set; } = null!;
}