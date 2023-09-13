using SP.Shared.Common.Feature.PackingList.Model;
using SP.Web.Site.Features.Item;
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

    public void DeleteGroup(string groupid)
    {
        var item = Groups.First(x => x.Id == groupid);
        Groups.Remove(item);
    }

    public void DeleteGroupItem(string groupid, string itemid)
    {
        var group = Groups.First(x => x.Id == groupid);
        var item = group.Items.First(x => x.Id == itemid);
        group.Items.Remove(item);
    }

    public void UpdateGroupName(string groupid, string name)
    {
        var item = Groups.First(x => x.Id == groupid);
        item.Name = name;
    }

    public void AddItemToGroup(string groupid, ItemViewModel vm)
    {
        var item = Groups.First(x => x.Id == groupid);
        item.Items.Add(new PackingListGroupItemViewModel
        {
            Name = vm.Name,
            Weight = vm.Weight.ToString(),
            WeightSufix = vm.WeightSufix,
            Id = vm.Id
        });
    }

    private List<PackingListGroupViewModel> GetGroups(List<PackListGroupModel> modelGroups)
    {
        var list = new List<PackingListGroupViewModel>();
        foreach (var item in modelGroups)
        {
            list.Add(new PackingListGroupViewModel
            {
                Id = item.Id,
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
                Id = item.RefId,
                Quantity = item.Quantity
            });
        }

        return list;
    }
}