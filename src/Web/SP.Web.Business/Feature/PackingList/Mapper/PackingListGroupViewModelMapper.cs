using SP.Shared.Common.Feature.PackingList.Model;
using SP.Web.Business.Feature.PackingList.ViewModel;

namespace SP.Web.Business.Feature.PackingList.Mapper;

public class PackingListGroupViewModelMapper
{
    public static PackingListGroupViewModel Map(PackingListGroupModel item)
    {
        return new PackingListGroupViewModel
        {
            Id = item.Id,
            Name = item.Name,
            Items = GetItems(item.Items),
        };
    }

    public static PackingListGroupModel Map(PackingListGroupViewModel item)
    {
        return new PackingListGroupModel
        {
            Id = item.Id,
            Name = item.Name,
            Items = GetItems(item.Items),
        };
    }

    private static List<PackingListGroupItemViewModel> GetItems(List<PackingListGroupItemModel> itemItems)
    {
        var list = new List<PackingListGroupItemViewModel>();
        foreach (var item in itemItems)
        {
            list.Add(PackingListGroupItemViewModelMapper.Map(item));
        }

        return list;
    }

    private static List<PackingListGroupItemModel> GetItems(List<PackingListGroupItemViewModel> itemItems)
    {
        var list = new List<PackingListGroupItemModel>();
        foreach (var item in itemItems)
        {
            list.Add(PackingListGroupItemViewModelMapper.Map(item));
        }

        return list;
    }
}