using SP.Shared.Common.Feature.PackingList.Model;
using SP.Web.Business.Feature.PackingList.ViewModel;

namespace SP.Web.Business.Feature.PackingList.Mapper;

public static class PackingListViewModelMapper
{
    public static PackingListViewModel Map(PackingListModel model)
    {
        var vm = new PackingListViewModel
        {
            Id = model.Id.ToString(),
            Name = model.Name,
            Groups = GetGroups(model.Groups),
            UserId = model.UserId.ToString(),
            IsPublic = model.IsPublic,
            Language = model.Language,
            Description = model.Description
        };
        if (string.IsNullOrEmpty(vm.Language))
        {
            vm.Language = "NoLang";
        }

        return vm;
    }

    public static PackingListModel Map(PackingListViewModel model)
    {
        return new PackingListModel
        {
            Id = model.Id.ToString(),
            Name = model.Name,
            Groups = GetGroups(model.Groups),
            UserId = Guid.Parse(model.UserId),
            IsPublic = model.IsPublic,
            Language = model.Language,
            Description = model.Description
        };
    }

    private static List<PackingListGroupViewModel> GetGroups(List<PackingListGroupModel> modelGroups)
    {
        var list = new List<PackingListGroupViewModel>();
        foreach (var item in modelGroups)
        {
            list.Add(PackingListGroupViewModelMapper.Map(item));
        }

        return list;
    }

    private static List<PackingListGroupModel> GetGroups(List<PackingListGroupViewModel> modelGroups)
    {
        var list = new List<PackingListGroupModel>();
        foreach (var item in modelGroups)
        {
            list.Add(PackingListGroupViewModelMapper.Map(item));
        }

        return list;
    }
}