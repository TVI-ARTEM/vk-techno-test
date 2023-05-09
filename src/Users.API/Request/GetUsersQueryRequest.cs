namespace Users.API.Request;

public record GetUsersQueryRequest(
    int Take,
    int Skip
);