using SP.Shared.Common.Feature.PackingList.Model;
using SP.Web.Business.Feature.PackingList.ViewModel;

namespace SP.Web.Business.Feature.PackingList.Mapper;

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
            IsPublic = model.IsPublic
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