namespace Scraper;

public class ScrapWebsite {
    private static readonly Dictionary<string, string?> JobOfferValues;
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
    
    public static Dictionary<string, string?> ScrapData(string url, Dictionary<string, string> query) {
        foreach (string key in query.Keys) {
            if (JobOfferValues.ContainsKey(key)) {
                JobOfferValues.Add(key, Scraper.Scrap(url, query[key]));
            }
        }
        return JobOfferValues;
    }

    public static void AddNewPosition(string positionName) {
        JobOfferValues.Add(positionName, null);
    }
}