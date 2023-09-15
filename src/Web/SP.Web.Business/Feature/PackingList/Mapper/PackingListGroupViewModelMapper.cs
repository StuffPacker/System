using SP.Shared.Common.Feature.PackingList.Model;
using SP.Web.Business.Feature.PackingList.ViewModel;

namespace SP.Web.Business.Feature.PackingList.Mapper;

public class PackingListGroupViewModelMapper
{
    public static PackingListGroupViewModel Map(PackListGroupModel item)
    {
        return new PackingListGroupViewModel
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
}