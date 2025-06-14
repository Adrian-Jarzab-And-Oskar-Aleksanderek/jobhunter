namespace Scraper.Models;

public class Meta
{
    public int Page { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int? PrevPage { get; set; }
    public int? NextPage { get; set; }
}