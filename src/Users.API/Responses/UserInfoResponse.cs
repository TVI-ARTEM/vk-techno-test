namespace Users.API.Responses;

public record UserInfo(
    long Id,
    string Login,
    DateTime CreatedDate,
    UserGroupInfo UserGroupInfo,
    UserStateInfo UserStateInfo
);