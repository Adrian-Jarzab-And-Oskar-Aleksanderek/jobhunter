using System.Text.Json;
using Backend.Data;
using Backend.Mappers;
using Backend.Service.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Backend.Controllers;


[Route("api/JobOffers")]
[ApiController]
public class JobOffersController :ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly RedisService _redisService;
    public JobOffersController(ApplicationDbContext context, IOptions<JsonOptions> jsonOptions, RedisService redisService)
    {
        _context = context;
        _jsonOptions = jsonOptions.Value.JsonSerializerOptions;
        _redisService = redisService;
    }

    [HttpGet("/api/search")]
    public async Task<IActionResult> GetSearchedJobOffers(
        [FromQuery] string? search,
        [FromQuery] int page,
        [FromQuery] string? experienceLevel,
        [FromQuery] string? employmentType,
        [FromQuery] string? minSalary,
        [FromQuery] string? maxSalary)
    {
        int resultsPerPage = 20;

        string cacheKey = $"Job_Offers_search_{search}_{page}_{experienceLevel}_{employmentType}_{minSalary}_{maxSalary}";

        var cachedJobOffersSearch = await _redisService.GetData(cacheKey);
        if (!string.IsNullOrEmpty(cachedJobOffersSearch))
        {
            string cachedETag = ETagService.GenerateETag(cachedJobOffersSearch);
            if (ETagService.IsETagValid(Request, cachedETag))
            {
                return StatusCode(StatusCodes.Status304NotModified);
            }

            Response.Headers.ETag = cachedETag;

            return Ok(JsonSerializer.Deserialize<object>(cachedJobOffersSearch));
        }

        var query = _context.JobOffers.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(j => EF.Functions.Like(j.Title.ToLower(), $"%{search.ToLower()}%"));
        }

        if (!string.IsNullOrEmpty(experienceLevel))
        {
            query = query.Where(j => j.ExperienceLevel.ToLower() == experienceLevel.ToLower());
        }

        if (!string.IsNullOrEmpty(employmentType))
        {
            query = query.Where(j => j.EmploymentTypes != null && j.EmploymentTypes.Any(et => et.Type.ToLower() == employmentType.ToLower()));
        }

        if (!string.IsNullOrEmpty(minSalary) && double.TryParse(minSalary, out double minSalaryValue))
        {
            query = query.Where(j => j.EmploymentTypes != null && j.EmploymentTypes.Any(et =>
                (et.From.HasValue && et.From.Value >= minSalaryValue) ||
                (et.To.HasValue && et.To.Value >= minSalaryValue)));
        }

        if (!string.IsNullOrEmpty(maxSalary) && double.TryParse(maxSalary, out double maxSalaryValue))
        {
            query = query.Where(j => j.EmploymentTypes != null && j.EmploymentTypes.Any(et =>
                (et.From.HasValue && et.From.Value <= maxSalaryValue) ||
                (et.To.HasValue && et.To.Value <= maxSalaryValue)));
        }

        var totalResults = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalResults / resultsPerPage) - 1;

        var jobOffers = await query
            .Include(j => j.Company)
            .Include(j => j.MultiLocation)
            .Include(j => j.EmploymentTypes)
            .Include(j => j.RequierdSkills)
            .ThenInclude(rs => rs.Skills)
            .OrderBy(j => j.Id)
            .Skip(resultsPerPage * page)
            .Take(resultsPerPage)
            .Select(j => j.MapToJobOfferDto())
            .ToListAsync();

        if (page > totalPages)
            return Redirect("" + totalPages);

        var response = new { jobOffers, totalPages, page };

        var jsonData = JsonSerializer.Serialize(response, _jsonOptions);
        string eTag = ETagService.GenerateETag(jsonData);

        Response.Headers.ETag = eTag;
        Response.Headers.CacheControl = "private";

        await _redisService.SaveData(cacheKey, JsonSerializer.Serialize(response, _jsonOptions), TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(30));

        return Ok(response);
    }


    [HttpGet("/api/offer")]
    public async Task<IActionResult> GetJobOfferById([FromQuery] int id)
    {
        string cacheKey = $"Job_Offer_id_{id}";

        var cachedJobOffer = await _redisService.GetData(cacheKey);
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
            .Include(j => j.Company)
            .Include(j => j.MultiLocation)
            .Include(j => j.EmploymentTypes)
            .Include(j => j.RequierdSkills)
            .ThenInclude(rs => rs.Skills)
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

        await _redisService.SaveData(cacheKey, jsonData, TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(60));

        return Ok(jobOffer);
    }
}
