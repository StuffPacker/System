using System.Security;

namespace SP.Web.Site.Features.PackingList;

public class PackingListGroupItemViewModel
{
    public string Name { get; set; } = string.Empty;

    public string Weight { get; set; } = string.Empty;

    public string WeightSufix { get; set; } = string.Empty;

    public string Id { get; set; } = string.Empty;

    public int Quantity { get; set; }
}