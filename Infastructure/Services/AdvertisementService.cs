using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Models.Entity;
using Domain.Models.Filters;
using Infastructure.Data;
using Microsoft.EntityFrameworkCore;
using WebAPIWithJWTAndIdentity.Response;

namespace Infrastructure.Services;

public class AdvertisementService(ApplicationDbContext context, IMapper mapper) : IAdvertisementService
{
    public async Task<Response<List<AdDto>>> GetAdvertisementAsync(AdFilter filter)
    {
        /*try
        {*/
        var query = context.Advertisements
            .AsQueryable();
            
        if (filter.Id.HasValue)
        {
            query = query.Where(x => x.Id == filter.Id.Value);
        }
        if (!string.IsNullOrEmpty(filter.Title))
        {
            query = query.Where(x => x.Title.Contains(filter.Title));
        }
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(x => x.Description.Contains(filter.Description));
        }
        if (filter.Price.HasValue)
        {
            query = query.Where(x => x.Price == filter.Price.Value);
        }
        if (filter.CreatedDate.HasValue)
        {
            query = query.Where(x => x.CreatedDate == filter.CreatedDate.Value);
        }
        if (filter.Status.HasValue)
        {
            query = query.Where(x => x.Status == filter.Status.Value);
        }

        var todoitem = await query.ToListAsync();
        var result = mapper.Map<List<AdDto>>(todoitem);
        return new Response<List<AdDto>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Advertisements retrieved successfully!",
            Data = result
        };
        /*}*/
        /*catch (Exception ex)
        {
            return new Response<List<TodoItemDto>>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }

    public async Task<Response<AdDto>> AddAdvertisementAsync(AdCreatDto adCreatDto)
    {
        /*try
        {*/
        var ad = mapper.Map<Advertisement>(adCreatDto);
        context.Advertisements.Add(ad);
        await context.SaveChangesAsync();
        var result = mapper.Map<AdDto>(ad);
        return new Response<AdDto>(HttpStatusCode.Created, "Advertisements created successfully!", result);
        /*}*/
        /*catch (Exception ex)
        {
            return new Response<TodoItemDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }

    public async Task<Response<AdDto>> UpdateAdvertisementAsync(AdDto adDto_)
    {
        /*try
       {*/
        var check = await context.Advertisements.FindAsync(adDto_.Id);
        if (check == null)
            return new Response<AdDto>(HttpStatusCode.NotFound, "Advertisements not found");
            
        check.Title = adDto_.Title;
        check.Description = adDto_.Description;
        check.Price = adDto_.Price;
        check.CreatedDate = adDto_.CreatedDate;
        check.Status = adDto_.Status;
        context.Advertisements.Update(check);
        await context.SaveChangesAsync();
        var result = mapper.Map<AdDto>(check);
        return new Response<AdDto>(HttpStatusCode.OK, "Advertisements updated successfully!", result);
        /*}*/
        /*catch (Exception ex)
        {
            return new Response<TodoItemDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }

    public async Task<Response<string>> DeleteAdvertisementAsync(int id)
    {
        /*try
        {*/
        var advertisements = await context.Advertisements.FindAsync(id);
        if (advertisements == null)
            return new Response<string>(HttpStatusCode.NotFound, "Advertisements not found");

        context.Advertisements.Remove(advertisements);
        await context.SaveChangesAsync();
        return new Response<string>(HttpStatusCode.OK, "Advertisements deleted successfully!");

        /*catch (Exception ex)
            {
                return new Response<string>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
            }*/    }

    public async Task<Response<AdDto>> GetAdvertisementByIdAsync(int id)
    {
        /*try
        {*/
        var advertisements = await context.Advertisements.FirstOrDefaultAsync(a => a.Id == id);
        if (advertisements == null)
            return new Response<AdDto>(HttpStatusCode.NotFound, "Advertisements not found");
            
        var result = mapper.Map<AdDto>(advertisements);
        return new Response<AdDto>(HttpStatusCode.OK, "Advertisements retrieved successfully!", result);
        
        /*catch (Exception ex)
        {
            return new Response<TodoItemDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }*/
    }
}