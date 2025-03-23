using FwksLabs.Boilerplate.Infra.Postgres;
using FwksLabs.Libs.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FwksLabs.Boilerplate.Tools.Migrations;

public sealed class MigrationContext : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var connectionString = args[0];

        if (connectionString.IsEmpty())
            throw new Exception("Connection String is missing. Check the values and try again.");

        var builder = new DbContextOptionsBuilder<DatabaseContext>()
            .UseNpgsql(connectionString, x =>
            {
                x.MigrationsHistoryTable("Migrations", "History");
                x.MigrationsAssembly(typeof(MigrationContext).Assembly.FullName);
            });

        return new DatabaseContext(builder.Options);
    }
}