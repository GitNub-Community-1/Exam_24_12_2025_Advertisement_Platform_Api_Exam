using Domain.Dtos;
using Domain.Models.Filters;
using WebAPIWithJWTAndIdentity.Response;

namespace Infrastructure.Services;

public interface IAdvertisementService
{
    public Task<Response<List<AdDto>>> GetAdvertisementAsync(AdFilter filter);
    public Task<Response<AdDto>> AddAdvertisementAsync(AdCreatDto todoItemAddDto);
    public Task<Response<AdDto>> UpdateAdvertisementAsync(AdDto todoItemDto);
    public Task<Response<string>> DeleteAdvertisementAsync(int id);
    public Task<Response<AdDto>> GetAdvertisementByIdAsync(int id);
}