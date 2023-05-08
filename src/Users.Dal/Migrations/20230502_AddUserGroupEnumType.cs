using FluentMigrator;


namespace Users.Dal.Migrations;

[Migration(20230502, TransactionBehavior.None)]
public class AddUserGroupEnumType : Migration {
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'user_group_enum') THEN
            CREATE TYPE user_group_enum AS ENUM('Admin', 'User');
        END IF;
    END
$$;";

        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS user_group_enum;
    END
$$;";

        Execute.Sql(sql);
    }
}