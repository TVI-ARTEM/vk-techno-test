using FluentMigrator;


namespace Users.Dal.Migrations;

[Migration(20230503, TransactionBehavior.None)]
public class AddUserStateEnumType : Migration {
    public override void Up()
    {
        const string sql = @"
DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'user_state_enum') THEN
            CREATE TYPE user_state_enum AS ENUM('Active', 'Blocked');
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
        DROP TYPE IF EXISTS user_state_enum;
    END
$$;";

        Execute.Sql(sql);
    }
}