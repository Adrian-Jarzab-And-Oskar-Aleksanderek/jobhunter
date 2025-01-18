namespace Scraper;

public class Meta
{
    public int Page { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public object PrevPage { get; set; }
    public int NextPage { get; set; }
}