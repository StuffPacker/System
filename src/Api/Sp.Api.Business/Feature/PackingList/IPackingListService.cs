using SP.Shared.Common.Feature.PackingList.Dto;

namespace Sp.Api.Business.Feature.PackingList;

public interface IPackingListService
{
    Task<PackingListDto> GetPackingListById(string id, Guid userId);

    Task<List<PackingListDto>> GetPackingLists(Guid userId);

    Task<PackingListDto> CreatePackingList(Guid userId);

    Task Delete(string id, Guid getUserId);

    Task Update(PackingListDto model);
}