using System.Text.Json;
using Backend.Data;
using Backend.Mappers;
using Backend.Service.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Options;

namespace Backend.Controllers;


[Route("api/JobOffers")]
[ApiController]
public class JobOffersControler :ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;
    public JobOffersControler(ApplicationDbContext context, IDistributedCache cache, IOptions<JsonOptions> jsonOptions)
    {
        _context = context;
        _cache = cache;
        _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
    }
    
    [HttpGet("/api/offers")]
    public async Task<IActionResult> GetAllJobOffers([FromQuery] int page)
    {
        int resultsPerPage = 20;
        
        string cacheKey = $"jobOffers{page}";
        
        var cachedJobOffers = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedJobOffers))
        {
            string cachedETag = ETagService.GenerateETag(cachedJobOffers);
            if (ETagService.IsETagValid(Request, cachedETag))
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }
            
            Response.Headers.ETag = cachedETag;
            
            return Ok(JsonSerializer.Deserialize<object>(cachedJobOffers)); 
        }

        int totalResults = _context.JobOffers.Count();
        int totalPages = (int)Math.Ceiling((double)totalResults / resultsPerPage) - 1;
    
        var jobOffers = _context.JobOffers
            .Skip(resultsPerPage * page)
            .Take(resultsPerPage)
            .Include(j => j.MultiLocation)
            .Include(j => j.EmploymentTypes)
            .ToList()
            .Select(jobOffer => jobOffer.MapToJobOfferDto())
            .ToList();

        if (page > totalPages)
            return Redirect("" + totalPages);
        
        var response = new { jobOffers, totalPages, page };
        
        var jsonData = JsonSerializer.Serialize(response, _jsonOptions);
        string eTag = ETagService.GenerateETag(jsonData);
        
        Response.Headers.ETag = eTag;
        Response.Headers.CacheControl = "private";
        
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromSeconds(30)
        };
        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(response,_jsonOptions), cacheOptions);
        
        return Ok(response);
    }


    [HttpGet("/api/offer")]
    public async Task<IActionResult> GetJobOfferById([FromQuery] int id)
    {
        string cacheKey = $"jobOffer_{id}";

        var cachedJobOffer = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedJobOffer))
        {
            string cachedETag = ETagService.GenerateETag(cachedJobOffer);
            if (ETagService.IsETagValid(Request, cachedETag))
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            Response.Headers.ETag = cachedETag;
            return Ok(JsonSerializer.Deserialize<object>(cachedJobOffer));
        }

        var jobOffer = _context.JobOffers
            .Include(j => j.Reviews)
            .Include(j => j.MultiLocation)
            .Include(j => j.EmploymentTypes)
            .FirstOrDefault(j => j.Id == id)?
            .MapToJobOfferDto();

        if (jobOffer == null)
        {
            return NotFound();
        }

        string jsonData = JsonSerializer.Serialize(jobOffer, _jsonOptions);
        string eTag = ETagService.GenerateETag(jsonData);
        
        Response.Headers.ETag = eTag;
        Response.Headers.CacheControl = "private";

        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            SlidingExpiration = TimeSpan.FromSeconds(60)
        };

        await _cache.SetStringAsync(cacheKey, jsonData, cacheOptions);

        return Ok(jobOffer);
    }
}