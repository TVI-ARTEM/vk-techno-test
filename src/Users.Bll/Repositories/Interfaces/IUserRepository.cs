using Users.Bll.Models;
using Users.Bll.Queries;

namespace Users.Bll.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> Query(long id, CancellationToken token);
    Task<User?> Query(string login, CancellationToken token);
    Task<IEnumerable<User>> Query(UserGroupEnum userGroup, CancellationToken token);
    Task<IEnumerable<User>> Query(QueryUserRequest request, CancellationToken token);
    Task<IEnumerable<User>> Query(CancellationToken token);

    Task Add(AddUserRequest userRequest, CancellationToken token);

    Task Remove(long id, CancellationToken token);
    Task Remove(string login, CancellationToken token);
}