using Microsoft.EntityFrameworkCore;

namespace SP.Web.Site;

public class StuffPackingDbContextMigrator
{
    public void Migrate(IServiceScope serviceScope, string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            return;
        }

        var optionsBuilder = new DbContextOptionsBuilder<SPWebSiteDataContext>();
        optionsBuilder.UseSqlServer(connectionString);

        using (var stoConnectDbContext =
               new SPWebSiteDataContext(optionsBuilder.Options))
        {
            if (!stoConnectDbContext.AllMigrationsApplied())
            {
                stoConnectDbContext.Database.Migrate();
            }
        }
    }
}