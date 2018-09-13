using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace AliasManager.Repository.Script.ddl
{
    public class AddTableAliases : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Create.Table("Aliases")
                  .WithColumn("Id")
                  .AsInt64()
                  .PrimaryKey()
                  .Identity()
                  .WithColumn("Alias")
                  .AsString(256)
                  .NotNullable()
                  .WithColumn("Email")
                  .AsString(256)
                  .NotNullable()
                  .WithColumn("Enabled")
                  .AsBoolean()
                  .NotNullable()
                  .WithDefaultValue(true);
        }
    }
}
