using Users.Bll.Models;
using Users.Bll.Queries;
using Users.Bll.Repositories.Interfaces;
using Users.Bll.Services.Interfaces;

namespace Users.Bll.Services;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> QueryUser(long id, CancellationToken token)
    {
        return await _userRepository.Query(id, token);
    }

    public async Task<User?> QueryUser(string login, CancellationToken token)
    {
        return await _userRepository.Query(login, token);
    }

    public async Task<IEnumerable<User>> QueryUsers(UserGroupEnum userGroup, CancellationToken token)
    {
        return await _userRepository.Query(userGroup, token);
    }

    public async Task<IEnumerable<User>> QueryUsers(QueryUserRequest request, CancellationToken token)
    {
        return await _userRepository.Query(request, token);
    }

    public async Task<IEnumerable<User>> QueryUsers(CancellationToken token)
    {
        return await _userRepository.Query(token);
    }

    public async Task Add(AddUserRequest userRequest, CancellationToken token)
    {
        await _userRepository.Add(userRequest, token);
    }

    public async Task RemoveUser(long id, CancellationToken token)
    {
        await _userRepository.Remove(id, token);
    }

    public async Task RemoveUser(string login, CancellationToken token)
    {
        await _userRepository.Remove(login, token);
    }
}