using AspNetCore.SEOHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SP.Web.Business;
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

    public string ConnectionString { get; set; } = null!;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddSeq(Configuration.GetSection("Seq"));
        });
        ConnectionString = Configuration.GetConnectionString("SPWebSiteDataContextConnection") ?? throw new InvalidOperationException("Connection string 'SPWebSiteDataContextConnection' not found.");
        services.AddDbContext<SPWebSiteDataContext>(options => options.UseSqlServer(ConnectionString));

        services.AddDbContext<SPWebSiteDataContext>(options => options.UseSqlServer(ConnectionString, ef => ef.MigrationsAssembly(typeof(SPWebSiteDataContext).Assembly.FullName)));

        services.AddIdentity<StuffPackerUser, IdentityRole>().AddEntityFrameworkStores<SPWebSiteDataContext>().AddDefaultTokenProviders();

        services.AddRazorPages();
        services.AddControllers().AddJsonOptions(options =>
        {
            // Global settings: use the defaults, but serialize enums as strings
            // (because it really should be the default)
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
        services.ConfigureApplicationCookie(conf =>
        {
            conf.LoginPath = "/login";
        });
        services.AddInfrastructureBusiness(Configuration);
        services.Configure<GoogleAnalytics>(Configuration.GetSection("GoogleAnalytics"));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        var logger = app.ApplicationServices.GetService<ILogger<Program>>();

        // app.UseExceptionHandler("/Error");
        // app.UseStatusCodePagesWithRedirects("/StatusCode/{0}");
        app.UseStatusCodePagesWithReExecute("/StatusCode", "?statusCode={0}");
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseXMLSitemap(env.ContentRootPath);
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