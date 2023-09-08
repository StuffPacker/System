using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SP.Web.Site;

public static class DbContextMigrationExtensions
{
    public static bool AllMigrationsApplied(this DbContext context)
    {
        try
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}