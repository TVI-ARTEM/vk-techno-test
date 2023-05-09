namespace Users.Bll.Queries;

public record QueryUserRequest(
    int Take,
    int Skip
);