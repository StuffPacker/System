using SP.Shared.Common.Feature.PackingList.Dto;
using SP.Shared.Common.Feature.PackingList.Model;

namespace SP.Shared.Common.Feature.PackingList.Mapper;

public class PackingListMapper : IPackingListMapper
{
    public PackingListDto Map(PackingListModel model)
    {
        return new PackingListDto
        {
            Name = model.Name,
            IsPublic = model.IsPublic,
            UserId = model.UserId.ToString(),
            Groups = GetGroups(model.Groups),
            Id = model.Id
        };
    }

    public IEnumerable<PackingListDto> Map(IEnumerable<PackingListModel> model)
    {
        var list = new List<PackingListDto>();
        foreach (var item in model)
        {
            list.Add(Map(item));
        }

        return list;
    }

    public PackingListModel Map(PackingListDto dto)
    {
        return new PackingListModel
        {
            Id = dto.Id,
            Name = dto.Name,
            UserId = Guid.Parse(dto.UserId),
            IsPublic = dto.IsPublic,
            Groups = GetGroups(dto.Groups)
        };
    }

    public IEnumerable<PackingListModel> Map(IEnumerable<PackingListDto> dto)
    {
        var list = new List<PackingListModel>();
        foreach (var item in dto)
        {
            list.Add(Map(item));
        }

        return list;
    }

    private List<PackingListDto> GetGroups(List<PackListGroupModel> modelGroups)
    {
        var list = new List<PackingListDto>();
        foreach (var item in modelGroups)
        {
            list.Add(new PackingListDto
            {
                Name = item.Name,
                Id = item.Id,
                Items = GetItems(item.Items)
            });
        }

        return list;
    }

    private List<PackingListGroupItemDto> GetItems(List<PackingListGroupItemModel> itemItems)
    {
        var list = new List<PackingListGroupItemDto>();
        foreach (var item in itemItems)
        {
            list.Add(new PackingListGroupItemDto
            {
                Id = item.RefId,
                Name = item.Name,
                Quantity = item.Quantity,
                WeightSufix = item.WeightSufix,
                Weight = item.Weight
            });
        }

        return list;
    }

    private List<PackListGroupModel> GetGroups(List<PackingListDto> dtoGroups)
    {
        var list = new List<PackListGroupModel>();
        foreach (var item in dtoGroups)
        {
            list.Add(new PackListGroupModel
            {
                Name = item.Name,
                Id = item.Id,
                Items = GetItems(item.Items)
            });
        }

        return list;
    }

    private List<PackingListGroupItemModel> GetItems(List<PackingListGroupItemDto> itemItems)
    {
        var list = new List<PackingListGroupItemModel>();
        foreach (var item in itemItems)
        {
            list.Add(new PackingListGroupItemModel
            {
                Name = item.Name,
                Quantity = item.Quantity,
                RefId = item.Id,
                Weight = item.Weight,
                WeightSufix = item.WeightSufix
            });
        }

        return list;
    }
}