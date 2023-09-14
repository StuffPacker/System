using SP.Shared.Common.Feature.PackingList.Model;
using SP.Web.Site.Features.PackingList;

namespace SP.Web.Site.Features.Packinglist;

public class PackingListGroupViewModel
{
    public string Name { get; set; } = string.Empty;

    public List<PackingListGroupItemViewModel> Items { get; set; } = null!;

    public string Id { get; set; } = string.Empty;
}