using AspNetCore.SEOHelper.Sitemap;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sp.Api.Client.Feature.Health;
using SP.Web.Business.Feature.Health;
using SP.Web.Business.Feature.PackingList;
using SP.Web.Business.Feature.User.GetUser;
using SP.Web.Business.Feature.User.GetUserList;

namespace SP.Web.Site.Features.Home;

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _env;
    private readonly IMediator _mediator;
    private readonly IPackingListService _packingListService;

    public HomeController(IWebHostEnvironment env, IMediator mediator, IPackingListService packingListService)
    {
        _env = env;
        _mediator = mediator;
        _packingListService = packingListService;
    }

    [Route("sitemap")]
    public async Task<string> Sitemap()
    {
        await CreateSitemapInRootDirectory();
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

    public async Task<string> CreateSitemapInRootDirectory()
    {
        var list = new List<SitemapNode>();
        list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://stuffpacker.net", Frequency = SitemapFrequency.Daily });

        // get users
        var users = await _mediator.Send(new GetUserListCommand(Guid.Empty));

        foreach (var user in users)
        {
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://stuffpacker.net/user/" + user.UserId, Frequency = SitemapFrequency.Daily });
        }

        // get public lists
        var publicLists = await _packingListService.GetPackingListsPublic();
        foreach (var item in publicLists)
        {
            if (item.IsPublic)
            {
                list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://stuffpacker.net/packinglist/" + item.Id, Frequency = SitemapFrequency.Daily });
            }
        }

        list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = "https://stuffpacker.net/Login", Frequency = SitemapFrequency.Daily });
        list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = "https://stuffpacker.net/account/register", Frequency = SitemapFrequency.Daily });
        new SitemapDocument().CreateSitemapXML(list, _env.WebRootPath);
        return "sitemap.xml file should be create in root directory";
    }
}