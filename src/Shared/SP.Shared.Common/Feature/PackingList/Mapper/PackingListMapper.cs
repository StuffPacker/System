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
            Groups = GetGroupsDto(model.Groups),
            Id = model.Id,
            Language = model.Language,
            Description = model.Description
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
        var language = string.Empty;
        if (dto.Language != null)
        {
            language = dto.Language;
        }

        return new PackingListModel
        {
            Id = dto.Id,
            Name = dto.Name,
            UserId = Guid.Parse(dto.UserId),
            IsPublic = dto.IsPublic,
            Groups = GetGroupsModel(dto.Groups),
            Language = language,
            Description = dto.Description
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

    private List<PackingListGroupDto> GetGroupsDto(List<PackingListGroupModel> modelGroups)
    {
        var list = new List<PackingListGroupDto>();
        foreach (var item in modelGroups)
        {
            list.Add(new PackingListGroupDto
            {
                Name = item.Name,
                Id = item.Id,
                Items = GetItemsDto(item.Items)
            });
        }

        return list;
    }

    private List<PackingListGroupItemDto> GetItemsDto(List<PackingListGroupItemModel> itemItems)
    {
        var list = new List<PackingListGroupItemDto>();
        foreach (var item in itemItems)
        {
            list.Add(new PackingListGroupItemDto
            {
                Name = item.Name,
                Quantity = item.Quantity,
                WeightSufix = item.WeightSufix,
                Weight = item.Weight,
                Id = item.RefId
            });
        }

        return list;
    }

    private List<PackingListGroupModel> GetGroupsModel(List<PackingListGroupDto> dtoGroups)
    {
        var list = new List<PackingListGroupModel>();
        foreach (var item in dtoGroups)
        {
            list.Add(new PackingListGroupModel
            {
                Name = item.Name,
                Id = item.Id,
                Items = GetItemsModel(item.Items)
            });
        }

        return list;
    }

    private List<PackingListGroupItemModel> GetItemsModel(List<PackingListGroupItemDto> itemItems)
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