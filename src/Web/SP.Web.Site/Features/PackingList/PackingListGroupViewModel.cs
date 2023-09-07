using SP.Web.Site.Features.PackingList;

namespace SP.Web.Site.Features.Packinglist;

public class PackingListGroupViewModel
{
    public string Name { get; set; } = string.Empty;

    public List<PackingListGroupItemViewModel> Items { get; set; } = null!;
}