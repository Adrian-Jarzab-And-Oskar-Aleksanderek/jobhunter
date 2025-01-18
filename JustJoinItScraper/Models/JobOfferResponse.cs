using Backend.Models;

namespace Scraper;

public class JobOfferResponse
{
    public List<JobOffer> Data { get; set; }
    public Meta Meta { get; set; }
}