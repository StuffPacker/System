using SP.Shared.Common.Feature.PackingList.Model;
using SP.Web.Site.Features.Packinglist;

namespace SP.Web.Site.Features.PackingList.PackingList.Mapper;

public static class PackingListViewModelMapper
{
    public static PackingListViewModel Map(PackingListModel model)
    {
        return new PackingListViewModel
        {
            Id = model.Id.ToString(),
            Name = model.Name,
            Groups = GetGroups(model.Groups),
            UserId = model.UserId.ToString(),
        };
    }

    private static List<PackingListGroupViewModel> GetGroups(List<PackListGroupModel> modelGroups)
    {
        var list = new List<PackingListGroupViewModel>();
        foreach (var item in modelGroups)
        {
            list.Add(PackingListGroupViewModelMapper.Map(item));
        }

        return list;
    }
}