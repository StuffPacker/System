using System.Security.Authentication;
using SP.Database.Mongo.Feature.PackingList;
using SP.Shared.Common.Feature.PackingList.Dto;
using SP.Shared.Common.Feature.PackingList.Mapper;
using SP.Shared.Common.Feature.PackingList.Model;

namespace Sp.Api.Business.Feature.PackingList;

public class PackingListService : IPackingListService
{
    private readonly IPackingListRepository _packingListRepository;
    private readonly IPackingListMapper _packingListMapper;

    public PackingListService(IPackingListRepository packingListRepository, IPackingListMapper packingListMapper)
    {
        _packingListRepository = packingListRepository;
        _packingListMapper = packingListMapper;
    }

    public async Task<PackingListDto> GetPackingListById(string id)
    {
      return await GetById(id);
    }

    public async Task<List<PackingListDto>> GetPackingLists(Guid userId)
    {
        var result = await _packingListRepository.GetByUserId(userId);
        var list = new List<PackingListDto>();
        foreach (var item in result)
        {
            list.Add(_packingListMapper.Map(item));
        }

        return list;
    }

    public async Task<PackingListDto> CreatePackingList(Guid userId, PackingListModel model)
    {
        var result = await _packingListRepository.Create(model);

        return _packingListMapper.Map(result);
    }

    public async Task Delete(string id, Guid userId)
    {
        var model = await _packingListRepository.GetById(id);
        if (!Access(_packingListMapper.Map(model), userId))
        {
            throw new AuthenticationException();
        }

        await _packingListRepository.Delete(id);
    }

    public async Task Update(PackingListDto viewModel)
    {
        var model = GetModel(viewModel);
        await _packingListRepository.Update(model);
    }

    private PackingListModel GetModel(PackingListDto viewModel)
    {
        return new PackingListModel
        {
            UserId = Guid.Parse(viewModel.UserId),
            Name = viewModel.Name,
            Groups = GetGroupModel(viewModel.Groups),
            Id = viewModel.Id,
            IsPublic = viewModel.IsPublic
        };
    }

    private List<PackListGroupModel> GetGroupModel(List<PackingListDto> viewModelGroups)
    {
        var list = new List<PackListGroupModel>();
        foreach (var item in viewModelGroups)
        {
            list.Add(new PackListGroupModel
            {
                Name = item.Name,
                Items = GetItems(item.Items),
                Id = item.Id
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
                WeightSufix = item.WeightSufix,
                RefId = item.Id,
                Weight = Convert.ToDecimal(item.Weight),
                Quantity = item.Quantity
            });
        }

        return list;
    }

    private async Task<PackingListDto> GetById(string id)
    {
        var result = await _packingListRepository.GetById(id);
        var vm = _packingListMapper.Map(result);

        return vm;
    }

    private bool Access(PackingListDto model, Guid userId)
    {
        if (Guid.Parse(model.UserId) == userId)
        {
            return true;
        }

        return false;
    }
}