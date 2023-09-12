using SP.Shared.Common.Feature.PackingList.Model;
using SP.Web.Site.Features.PackingList;

namespace SP.Web.Site.Features.Packinglist;

public class PackingListViewModel
{
    public PackingListViewModel(PackingListModel model)
    {
        Id = model.Id.ToString();
        Name = model.Name;
        Groups = GetGroups(model.Groups);
        UserId = model.UserId.ToString();
    }

    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public List<PackingListGroupViewModel> Groups { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;

    private List<PackingListGroupViewModel> GetGroups(List<PackListGroupModel> modelGroups)
    {
        var list = new List<PackingListGroupViewModel>();
        foreach (var item in modelGroups)
        {
            list.Add(new PackingListGroupViewModel
            {
                Name = item.Name,
                Items = GetItems(item.Items)
            });
        }

        return list;
    }

    private List<PackingListGroupItemViewModel> GetItems(List<PackingListGroupItemModel> itemItems)
    {
        var list = new List<PackingListGroupItemViewModel>();
        foreach (var item in itemItems)
        {
            list.Add(new PackingListGroupItemViewModel
            {
                Name = item.Name,
                Weight = item.Weight.ToString(),
                WeightSufix = item.WeightSufix,
            });
        }

        return list;
    }
}