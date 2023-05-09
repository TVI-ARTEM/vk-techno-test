namespace Users.API.Request;

public record AddUserRequest
(
    string Login,
    string Password,
    string GroupCode,
    string GroupDescription,
    string StateDescription
);