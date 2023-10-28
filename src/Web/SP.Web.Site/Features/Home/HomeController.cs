using AspNetCore.SEOHelper.Sitemap;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SP.Web.Business.Feature.Health;

namespace SP.Web.Site.Features.Home;

public class HomeController : Controller
{
    private readonly IMediator _mediator;
    private readonly IWebHostEnvironment _env;

    public HomeController(IMediator mediator, IWebHostEnvironment env)
    {
        _mediator = mediator;
        _env = env;
    }

    [Route("health")]
    public async Task<string> Health()
    {
        var result = await _mediator.Send(new HealthCommand());
        return result;
    }

    [Route("sitemap")]
    public async Task<string> Sitemap()
    {
        await Task.Delay(1);
        CreateSitemapInRootDirectory();
        return "done!";
    }

    [Route("")]
    public ActionResult Index()
    {
        if (User.Identity != null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("Index");
            }
        }

        return View("Home");
    }

    public string CreateSitemapInRootDirectory()
    {
        var list = new List<SitemapNode>();
        list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://beta.stuffpacker.net", Frequency = SitemapFrequency.Daily });
        list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://beta.stuffpacker.net/packinglist/6509405ca3246bbcf127dfaa/public", Frequency = SitemapFrequency.Daily });
        list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = "https://beta.stuffpacker.net/Login", Frequency = SitemapFrequency.Daily });
        list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = "https://beta.stuffpacker.net/account/register", Frequency = SitemapFrequency.Daily });
        new SitemapDocument().CreateSitemapXML(list, _env.ContentRootPath);
        return "sitemap.xml file should be create in root directory";
    }
}