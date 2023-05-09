using Users.Bll.Models;

namespace Users.Bll.Queries;

public record AddUserRequest(
    string Login,
    string Password,
    DateTime CreatedDate,
    UserGroupEnum GroupCode,
    string GroupDescription,
    string StateDescription
);