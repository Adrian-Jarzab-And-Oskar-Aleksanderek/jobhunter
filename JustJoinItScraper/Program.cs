using System;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Scraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("authority", "api.justjoin.it");
            client.DefaultRequestHeaders.Add("method", "GET");
            client.DefaultRequestHeaders.Add("path",
                "/v2/user-panel/offers?&page=2&sortBy=published&orderBy=DESC&perPage=100&salaryCurrencies=PLN");
            client.DefaultRequestHeaders.Add("scheme", "https");
            client.DefaultRequestHeaders.Add("accept", "application/json, text/plain, */*");
            client.DefaultRequestHeaders.Add("accept-encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("accept-language", "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
            client.DefaultRequestHeaders.Add("origin", "https://justjoin.it");
            client.DefaultRequestHeaders.Add("priority", "u=1, i");
            client.DefaultRequestHeaders.Add("referer", "https://justjoin.it/");
            client.DefaultRequestHeaders.Add("sec-ch-ua",
                "\"Chromium\";v=\"130\", \"Google Chrome\";v=\"130\", \"Not?A_Brand\";v=\"99\"");
            client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
            client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
            client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
            client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
            client.DefaultRequestHeaders.Add("sec-fetch-site", "same-site");
            client.DefaultRequestHeaders.Add("version", "2");
            client.DefaultRequestHeaders.Add("x-ga", "GA1.1.366351086.1728371165");
            client.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");
            using var scope = serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            API_GET api = new API_GET(client);
            int i = 1;
            int pages = 0;
            do
            {

                string url = "https://api.justjoin.it/v2/user-panel/offers?&page=" + i +
                             "&sortBy=published&orderBy=DESC&perPage=100&salaryCurrencies=PLN";
                try
                {
                    HttpResponseMessage response = await api.GetResponseAsync(url);

                    string content = await api.DecodeResponseContentAsync(response);

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine($"Status Code: {response.StatusCode}");
                        break;
                    }

                    try
                    {
                        var jobOffers = JsonConvert.DeserializeObject<JobOfferResponse>(content);
                        pages = jobOffers.Meta.TotalPages;

                        foreach (var offer in jobOffers.Data)
                        {
                            dbContext.JobOffers.Add(offer);
                        }
                    }
                    catch (JsonReaderException jre)
                    {
                        Console.WriteLine($"Deserialization error: {jre.Message}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Request error: {ex.Message}");
                }
                i++;
                
            } while (i != pages);
            await dbContext.SaveChangesAsync();
        }
        static void ConfigureServices(IServiceCollection services)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(
                        "Server=baza-projektowa.postgres.database.azure.com;Database=postgres;Port=5432;User Id=postgres;Password=TRYONE"));
            }
        }
    }
