using Users.Bll.Models;
using Users.Bll.Queries;

namespace Users.Bll.Services.Interfaces;

public interface IUserService
{
    Task<User?> QueryUser(long id, CancellationToken token);
    Task<User?> QueryUser(string login, CancellationToken token);
    Task<IEnumerable<User>> QueryUsers(UserGroupEnum userGroup, CancellationToken token);
    Task<IEnumerable<User>> QueryUsers(QueryUserRequest request, CancellationToken token);
    Task<IEnumerable<User>> QueryUsers(CancellationToken token);

    Task AddUser(AddUserRequest userRequest, CancellationToken token);

    Task RemoveUser(long id, CancellationToken token);
    Task RemoveUser(string login, CancellationToken token);
}