namespace Users.API.Requests;

public record GetUsersQueryRequest(
    int Take,
    int Skip
);