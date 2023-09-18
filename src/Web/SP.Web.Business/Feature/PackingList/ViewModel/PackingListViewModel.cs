using SP.Web.Business.Feature.Item;

namespace SP.Web.Business.Feature.PackingList.ViewModel;

public class PackingListViewModel
{
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
        var group = Groups.First(x => x.Id == groupid);
        var item = group.Items.FirstOrDefault(x => x.Id == vm.Id);
        if (item != null)
        {
            item.Quantity++;
            return;
        }

        group.Items.Add(new PackingListGroupItemViewModel
        {
            Name = vm.Name,
            Weight = vm.Weight.ToString(),
            WeightSufix = vm.WeightSufix,
            Id = vm.Id
        });
    }

    public void UpdateGroupItemQuantity(string groupId, string itemId, int quantity)
    {
        var group = Groups.First(x => x.Id == groupId);
        var item = group.Items.First(x => x.Id == itemId);
        item.Quantity = quantity;
    }
}