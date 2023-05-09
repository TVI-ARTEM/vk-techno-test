namespace Users.API.Response;

public record UserInfo(
    long Id,
    string Login,
    DateTime CreatedDate,
    UserGroupInfo UserGroupInfo,
    UserStateInfo UserStateInfo
);