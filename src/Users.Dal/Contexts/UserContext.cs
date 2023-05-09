using Microsoft.EntityFrameworkCore;
using Npgsql;
using Users.Bll.Models;

namespace Users.Dal.Contexts;

public class UserContext : DbContext
{
    static UserContext()
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<UserGroupEnum>();
        NpgsqlConnection.GlobalTypeMapper.MapEnum<UserStateEnum>();
    }

    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserGroup> UserGroups { get; set; } = null!;
    public DbSet<UserState> UserStates { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(
                "User ID=vk-users;Password=123456;Host=localhost;Port=15433;Database=vk-users;Pooling=true;"
            );
    }
}