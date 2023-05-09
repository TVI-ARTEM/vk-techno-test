using NpgsqlTypes;

namespace Users.Bll.Models;

public enum UserGroupEnum
{
    [PgName("Admin")] Admin,
    [PgName("User")] User
}