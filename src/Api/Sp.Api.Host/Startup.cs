using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SP.Shared.Common.Feature.Jwt;

namespace Sp.Api.Host;

public class Startup
{
    private readonly IWebHostEnvironment _env;

    public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
    {
        Configuration = configuration;
        _env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var key = Configuration.GetValue<string>("JwtOptions:Key");

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Stuffpacker.net",
                    ValidAudience = "Stuffpacker.net",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
                };
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        var logger = app.ApplicationServices.GetService<ILogger<Program>>();
        logger!.LogWarning("Sp.Api.Host Starting");
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseAuthentication();

        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller=Health}/{action=Index}/{id?}");
        });
    }
}