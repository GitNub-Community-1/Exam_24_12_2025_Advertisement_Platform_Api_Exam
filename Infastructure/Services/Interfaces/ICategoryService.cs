using Domain.Dtos;
using Domain.Models.Filters;
using WebAPIWithJWTAndIdentity.Response;

namespace Infrastructure.Services;

public interface ICategoryService
{
    public Task<Response<List<CategoryDto>>> GetCategoryAsync(CategoryFilter filter);
    public Task<Response<CategoryDto>> AddCategoryAsync(CategoryCreatDto todoItemAddDto);
    public Task<Response<CategoryDto>> UpdateCategoryAsync(CategoryDto todoItemDto);
    public Task<Response<string>> DeleteCategoryAsync(int id);
    public Task<Response<CategoryDto>> GetCategoryByIdAsync(int id);
}