using System.Security.Authentication;
using SP.Database.Mongo.Feature.PackingList;
using SP.Shared.Common.Feature.PackingList.Dto;
using SP.Shared.Common.Feature.PackingList.Mapper;
using SP.Shared.Common.Feature.PackingList.Model;

namespace Sp.Api.Business.Feature.PackingList;

public class PackingListService : IPackingListService
{
    private readonly IPackingListMapper _packingListMapper;
    private readonly IPackingListRepository _packingListRepository;

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

    public async Task Update(PackingListDto dto)
    {
        var model = _packingListMapper.Map(dto);
        await _packingListRepository.Update(model);
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