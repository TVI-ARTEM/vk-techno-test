using FluentMigrator;

namespace Users.Dal.Migrations;

[Migration(20230504, TransactionBehavior.None)]
public class InitSchema : Migration {
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsInt64().PrimaryKey("users_pk").Identity()
            .WithColumn("login").AsString().Unique().NotNullable()
            .WithColumn("password").AsString().NotNullable()
            .WithColumn("created_date").AsDateTimeOffset().NotNullable()
            .WithColumn("user_group_id").AsInt64().NotNullable()
            .WithColumn("user_state_id").AsInt64().NotNullable();
        
        Create.Table("user_groups")
            .WithColumn("id").AsInt64().PrimaryKey("user_groups_pk").Identity()
            .WithColumn("code").AsCustom("user_group_enum").NotNullable()
            .WithColumn("description").AsString().NotNullable();
        
        Create.Table("user_states")
            .WithColumn("id").AsInt64().PrimaryKey("user_states_pk").Identity()
            .WithColumn("code").AsCustom("user_state_enum").NotNullable()
            .WithColumn("description").AsString().NotNullable();

        Create.ForeignKey()
            .FromTable("users").ForeignColumn("user_group_id")
            .ToTable("user_groups").PrimaryColumn("id");
        
        Create.ForeignKey()
            .FromTable("users").ForeignColumn("user_state_id")
            .ToTable("user_states").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete.Table("users");
        Delete.Table("user_groups");
        Delete.Table("user_states");
    }
}