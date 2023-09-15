namespace SP.Web.Business.Feature.PackingList.ViewModel;

public class PackingListGroupViewModel
{
    public string Name { get; set; } = string.Empty;

    public List<PackingListGroupItemViewModel> Items { get; set; } = null!;

    public string Id { get; set; } = string.Empty;
}