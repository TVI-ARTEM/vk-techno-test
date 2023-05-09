using NpgsqlTypes;

namespace Users.Bll.Models;

public enum UserStateEnum
{
    [PgName("Active")] Active,
    [PgName("Blocked")] Blocked
}