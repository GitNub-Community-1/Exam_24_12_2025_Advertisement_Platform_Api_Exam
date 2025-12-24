using System.Net;
using Domain.Dtos;
using Domain.Models.Filters;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIWithJWTAndIdentity.Response;

namespace WebAPIWithJWTAndIdentity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService _categoryService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<Response<List<CategoryDto>>>> GetCategories(
        [FromQuery] CategoryFilter filter)
    {
        var response = await _categoryService.GetCategoryAsync(filter);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<CategoryDto>>> GetCategoryById(int id)
    {
        if (id <= 0)
            return BadRequest(new Response<CategoryDto>(
                HttpStatusCode.BadRequest, "Invalid ID"));

        var response = await _categoryService.GetCategoryByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Response<CategoryDto>>> AddCategory(
        [FromBody] CategoryCreatDto categoryCreatDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<CategoryDto>(
               HttpStatusCode.BadRequest, "Invalid data", null));

        var response = await _categoryService.AddCategoryAsync(categoryCreatDto);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<Response<CategoryDto>>> UpdateCategory(
        [FromBody] CategoryDto categoryDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<CategoryDto>(
             HttpStatusCode.BadRequest, "Invalid data", null));

        if (categoryDto.Id <= 0)
            return BadRequest(new Response<CategoryDto>(
               HttpStatusCode.BadRequest, "ID is required for update"));

        var response = await _categoryService.UpdateCategoryAsync(categoryDto);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<string>>> DeleteCategory(int id)
    {
        if (id <= 0)
            return BadRequest(new Response<string>(
                HttpStatusCode.BadRequest, "Invalid ID"));

        var response = await _categoryService.DeleteCategoryAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}