namespace Scraper;

public class ScrapWebsite {
    private static string _url;
    private static readonly Dictionary<string, string?> JobOfferValues;
    private static Dictionary<string, string> _query;
    static ScrapWebsite()
    {
        JobOfferValues = new Dictionary<string, string?>
        {
        { "companyName", null },
        { "salaryRange", null },
        { "salaryMax", null },
        { "salaryMin", null },
        { "locationsName", null },
        { "remote", null },
        { "expired", null },
        { "technologyStack", null },
        { "employmentType", null },
        { "experienceType", null },
        { "workType", null }
        };
    }

    public ScrapWebsite(string url, Dictionary<string, string> query) {
        _url = url;
        _query = query; 
    }

    public static Dictionary<string, string?> ScrapData() {
        foreach (string key in _query.Keys) {
            if (JobOfferValues.ContainsKey(key)) {
                JobOfferValues.Add(key, Scraper.Scrap(_url, _query[key]));
            }
        }
        return JobOfferValues;
    }

    public static void AddNewPosition(string positionName) {
        JobOfferValues.Add(positionName, null);
    }
}