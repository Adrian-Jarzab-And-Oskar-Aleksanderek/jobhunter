namespace Scraper;

internal class Program
{
    public static void Main(string[] args)
    {
        Dictionary<string, string> query = new()
        {
        { "companyName", "div[data-test=\"text-earningAmount\"" },
        { "salaryRange", "div[data-test=\"text-earningAmount\"" },
        { "salaryMax", "div[data-test=\"text-earningAmount\"" },
        { "salaryMin", "div[data-test=\"text-earningAmount\"" },
        { "locationsName", "div[data-test=\"text-earningAmount\"" },
        { "remote", "div[data-test=\"text-earningAmount\"" },
        { "expired", "div[data-test=\"text-earningAmount\"" },
        { "technologyStack", "div[data-test=\"text-earningAmount\"" },
        { "employmentType", "div[data-test=\"text-earningAmount\"" },
        { "experienceType", "div[data-test=\"text-earningAmount\"" },
        { "workType", "div[data-test=\"text-earningAmount\""}
        };
            
        var result = ScrapWebsite.ScrapData("https://www.pracuj.pl/praca/data-analyst-warszawa,oferta,1003663409?sug=list_top_cr_bd_3_tname_214_tgroup_A&s=1f7c2c91&searchId=MTczMDEzMjcwNzA4NC4xNDk0", query);
        foreach (var x in result) {
            Console.WriteLine(x.Value);
        }

        string z = "asdadsasd";
        z.Length
    }
}