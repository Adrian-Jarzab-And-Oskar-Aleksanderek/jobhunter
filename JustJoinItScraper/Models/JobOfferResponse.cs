using Backend.Models.JobOffer;
namespace Scraper.Models;

public class JobOfferResponse
{
    public List<JobOffer> Data { get; set; }
    public Meta Meta { get; set; }
}