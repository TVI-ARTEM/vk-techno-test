namespace Users.API.Requests;

public record AddUserRequest
(
    string Login,
    string Password,
    string GroupCode,
    string GroupDescription,
    string StateDescription
);