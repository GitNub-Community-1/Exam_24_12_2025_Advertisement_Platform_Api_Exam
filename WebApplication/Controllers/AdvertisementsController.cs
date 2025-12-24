using Domain.Dtos;
using Domain.Models.Filters;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using WebAPIWithJWTAndIdentity.Response;

namespace WebAPIWithJWTAndIdentity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdvertisementController(IAdvertisementService _advertisementService) : ControllerBase
{
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<Response<List<AdDto>>>> GetAdvertisements(
        [FromQuery] AdFilter filter)
    {
        var response = await _advertisementService.GetAdvertisementAsync(filter);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<AdDto>>> GetAdvertisementById(int id)
    {
        if (id <= 0)
            return BadRequest(new Response<AdDto>(
                HttpStatusCode.BadRequest, "Invalid ID"));

        var response = await _advertisementService.GetAdvertisementByIdAsync(id);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Response<AdDto>>> AddAdvertisement(
        [FromBody] AdCreatDto adCreatDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<AdDto>(
                HttpStatusCode.BadRequest, "Invalid data"));

        var response = await _advertisementService.AddAdvertisementAsync(adCreatDto);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize(Roles = "Admin,Moderator")]
    [HttpPut]
    public async Task<ActionResult<Response<AdDto>>> UpdateAdvertisement(
        [FromBody] AdDto adDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<AdDto>(
               HttpStatusCode.BadRequest, "Invalid data"));

        if (adDto.Id <= 0)
            return BadRequest(new Response<AdDto>(
            HttpStatusCode.BadRequest, "ID is required for update"));

        var response = await _advertisementService.UpdateAdvertisementAsync(adDto);
        return StatusCode(response.StatusCode, response);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<string>>> DeleteAdvertisement(int id)
    {
        if (id <= 0)
            return BadRequest(new Response<string>(
                HttpStatusCode.BadRequest, "Invalid ID"));

        var response = await _advertisementService.DeleteAdvertisementAsync(id);
        return StatusCode(response.StatusCode, response);
    }
}