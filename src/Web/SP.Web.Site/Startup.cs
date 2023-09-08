using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SP.Web.Site.Data;
using SP.Web.Site.Features.Item;
using SP.Web.Site.Features.PackingList;
using SP.Web.Site.Model;

namespace SP.Web.Site;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;

        // Force Migration
        var migrationConnection =
            Configuration.GetConnectionString("SPWebSiteDataContextConnection");
        new StuffPackingDbContextMigrator().Migrate(null!, migrationConnection!);
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Configuration.GetConnectionString("SPWebSiteDataContextConnection") ?? throw new InvalidOperationException("Connection string 'SPWebSiteDataContextConnection' not found.");

        services.AddDbContext<SPWebSiteDataContext>(options => options.UseSqlServer(connectionString));

        services.AddDbContext<SPWebSiteDataContext>(options => options.UseSqlServer(connectionString, ef => ef.MigrationsAssembly(typeof(SPWebSiteDataContext).Assembly.FullName)));

        services.AddIdentity<StuffPackerUser, IdentityRole>().AddEntityFrameworkStores<SPWebSiteDataContext>().AddDefaultTokenProviders();

        services.AddSingleton<IItemService, ItemServiceFake>();
        services.AddSingleton<IPackingListService, PackingListServiceFake>();
        services.AddRazorPages();
        services.AddControllers().AddJsonOptions(options =>
        {
            // Global settings: use the defaults, but serialize enums as strings
            // (because it really should be the default)
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}");
        });
    }
}