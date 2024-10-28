using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Scraper;

public static class Scraper
{
    private static readonly IWebDriver Driver = new ChromeDriver();
    
    public static string Scrap(string websiteUrl, string query) 
    {
        StringBuilder sb = new();
        NavigateToUrl(websiteUrl);
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
        finally
        {
            Close();
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
}