using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Formatters;
using SP.Web.Site.Features.Item;
using SP.Web.Site.Features.PackingList;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Global settings: use the defaults, but serialize enums as strings
    // (because it really should be the default)
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddSingleton<IItemService, ItemServiceFake>();
builder.Services.AddSingleton<IPackingListService, PackingListServiceFake>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.Run();