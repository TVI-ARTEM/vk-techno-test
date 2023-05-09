using Microsoft.EntityFrameworkCore;
using Users.Bll.Models;
using Users.Bll.Queries;
using Users.Bll.Repositories.Interfaces;
using Users.Dal.Contexts;

namespace Users.Dal.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context;
    }


    public async Task<User?> Query(long id, CancellationToken token)
    {
        return await _context.Users.FirstOrDefaultAsync(it => it.Id == id, token);
    }

    public async Task<User?> Query(string login, CancellationToken token)
    {
        return await _context.Users.FirstOrDefaultAsync(it => it.Login.ToLower() == login.ToLower(), token);
    }

    public async Task<IEnumerable<User>> Query(UserGroupEnum userGroup, CancellationToken token)
    {
        return await _context.Users.Where(it => it.UserGroup.Code == userGroup).ToListAsync(token);
    }

    public async Task<IEnumerable<User>> Query(QueryUserRequest request, CancellationToken token)
    {
        return await _context.Users.Skip(request.Skip).Take(request.Take).ToListAsync(token);
    }

    public async Task<IEnumerable<User>> Query(CancellationToken token)
    {
        return await _context.Users.ToListAsync(token);
    }

    public async Task Add(AddUserRequest userRequest, CancellationToken token)
    {
        var userGroup = new UserGroup
        {
            Code = userRequest.GroupCode,
            Description = userRequest.GroupDescription
        };

        var userState = new UserState
        {
            Code = UserStateEnum.Active,
            Description = userRequest.StateDescription
        };

        var user = new User
        {
            Login = userRequest.Login,
            Password = userRequest.Password,
            CreatedDate = userRequest.CreatedDate.ToUniversalTime(),
            UserGroup = userGroup,
            UserState = userState
        };

        await _context.UserGroups.AddAsync(userGroup, token);
        await _context.UserStates.AddAsync(userState, token);
        await _context.Users.AddAsync(user, token);
        await _context.SaveChangesAsync(token);
    }

    public async Task Remove(long id, CancellationToken token)
    {
        var userState = await _context.UserStates.FirstOrDefaultAsync(it => it.User.Id == id, token);
        if (userState != null)
        {
            userState.Code = UserStateEnum.Blocked;
            await _context.SaveChangesAsync(token);
        }
    }

    public async Task Remove(string login, CancellationToken token)
    {
        var userState = await _context.UserStates.FirstOrDefaultAsync(
            it => it.User.Login.ToLower() == login.ToLower(), token
        );

        if (userState != null)
        {
            userState.Code = UserStateEnum.Blocked;
            await _context.SaveChangesAsync(token);
        }
    }
}