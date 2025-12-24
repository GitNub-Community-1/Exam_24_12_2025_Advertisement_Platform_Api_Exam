using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Models.Entity;
using Domain.Models.Filters;
using Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using WebAPIWithJWTAndIdentity.Response;

namespace Infrastructure.Services;

public class CategoryService(ApplicationDbContext context, IMapper mapper) : ICategoryService
{
    public async Task<Response<List<CategoryDto>>> GetCategoryAsync(CategoryFilter filter)
    {
        /*try
       {*/
        var query = context.Categories
            .AsQueryable();
            
        if (filter.Id.HasValue)
        {
            query = query.Where(x => x.Id == filter.Id.Value);
        }
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.Contains(filter.Name));
        }
        var todoitem = await query.ToListAsync();
        var result = mapper.Map<List<CategoryDto>>(todoitem);
        return new Response<List<CategoryDto>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Category retrieved successfully!",
            Data = result
        };
        /*}*/
        /*catch (Exception ex)
        {
            return new Response<List<TodoItemDto>>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }

    public async Task<Response<CategoryDto>> AddCategoryAsync(CategoryCreatDto todoItemAddDto)
    {
        /*try
       {*/
        var todoItem = mapper.Map<Category>(todoItemAddDto);
        context.Categories.Add(todoItem);
        await context.SaveChangesAsync();
        var result = mapper.Map<CategoryDto>(todoItem);
        return new Response<CategoryDto>(HttpStatusCode.Created, "Category created successfully!", result);
        /*}*/
        /*catch (Exception ex)
        {
            return new Response<TodoItemDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }

    public async Task<Response<CategoryDto>> UpdateCategoryAsync(CategoryDto todoItemDto)
    {
        /*try
       {*/
        var check = await context.Categories.FindAsync(todoItemDto.Id);
        if (check == null)
            return new Response<CategoryDto>(HttpStatusCode.NotFound, "Category not found");
            
        check.Name = todoItemDto.Name;
        context.Categories.Update(check);
        await context.SaveChangesAsync();
        var result = mapper.Map<CategoryDto>(check);
        return new Response<CategoryDto>(HttpStatusCode.OK, "Category updated successfully!", result);
        /*}*/
        /*catch (Exception ex)
        {
            return new Response<TodoItemDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }

    public async Task<Response<string>> DeleteCategoryAsync(int id)
    {
        /*try
        {*/
        var category = await context.Categories.FindAsync(id);
        if (category == null)
            return new Response<string>(HttpStatusCode.NotFound, "Category not found");

        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "Category deleted successfully!");

        /*catch (Exception ex)
            {
                return new Response<string>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
            }*/
    }

    public async Task<Response<CategoryDto>> GetCategoryByIdAsync(int id)
    {
        /*try
       {*/
        var category = await context.Categories.FirstOrDefaultAsync(a => a.Id == id);
        if (category == null)
            return new Response<CategoryDto>(HttpStatusCode.NotFound, "Category not found");
            
        var result = mapper.Map<CategoryDto>(category);
        return new Response<CategoryDto>(HttpStatusCode.OK, "Category retrieved successfully!", result);
        
        /*catch (Exception ex)
        {
            return new Response<TodoItemDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }
}