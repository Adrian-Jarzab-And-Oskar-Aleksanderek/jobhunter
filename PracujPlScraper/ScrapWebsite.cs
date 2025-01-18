using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace PracujPlScraper;

public class ScrapWebsite {
    private static readonly Dictionary<string, string?> JobOfferValues;
    private static readonly IWebDriver Driver = new ChromeDriver();

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
        NavigateToUrl(url);
        foreach (string key in query.Keys) {
            if (!JobOfferValues.ContainsKey(key)) {
                continue;
            }
            var x = QueryResult(query[key]);
            Console.WriteLine($"{key}: {x}");
            JobOfferValues[key] = x;
        }
        Close();
        return JobOfferValues;
    }
    private static string QueryResult(string query) {
        StringBuilder sb = new();
        try
        {
            var jobListings = Driver.FindElements(By.CssSelector(query));
            foreach (var job in jobListings)
            {
                sb.Append(job.Text);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while scraping: " + ex.Message);
        }
        return sb.ToString();
    }

    private static void Close() 
    {
        Driver.Quit();
    }
    private static void NavigateToUrl(string websiteUrl)
    {
        Driver.Navigate().GoToUrl(websiteUrl);
    }
    public static void AddNewPosition(string positionName) {
        JobOfferValues.Add(positionName, null);
    }
}