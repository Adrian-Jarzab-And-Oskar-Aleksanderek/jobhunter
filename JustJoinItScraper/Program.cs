using Backend.Data;
using Backend.Models.JobOffer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Scraper.Models;

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
            int page = 1, totalPages = 0;
            
            var existingSkills = dbContext.Skills
                .ToDictionary(s => s.Name, s => s);

            List<Skill> newSkills = new List<Skill>();
            List<JobOffer> jobOffersToAdd = new List<JobOffer>();
            do
            {
                string url = $"https://api.justjoin.it/v2/user-panel/offers?page={page}&sortBy=published&orderBy=DESC&perPage=100&salaryCurrencies=PLN";
                try
                {
                    HttpResponseMessage response = await api.GetResponseAsync(url);
                    string content = await api.DecodeResponseContentAsync(response);

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine($"Status Code: {response.StatusCode}");
                        break;
                    }

                    var jobOffers = JsonConvert.DeserializeObject<JobOfferResponse>(content);
                    if (jobOffers == null || jobOffers.Meta == null)
                    {
                        Console.WriteLine("Deserialization returned null or Meta is null.");
                        continue;
                    }

                    totalPages = jobOffers.Meta.TotalPages;

                    foreach (var offer in jobOffers.Data)
                    {
                        var newJobOffer = new JobOffer
                        {
                            Slug = offer.Slug,
                            Title = offer.Title,
                            WorkplaceType = offer.WorkplaceType,
                            WorkingTime = offer.WorkingTime,
                            ExperienceLevel = offer.ExperienceLevel,
                            CategoryId = offer.CategoryId,
                            City = offer.City,
                            Street = offer.Street,
                            Latitude = offer.Latitude,
                            Longitude = offer.Longitude,
                            RemoteInterview = offer.RemoteInterview,
                            CompanyName = offer.CompanyName,
                            CompanyLogoThumbUrl = offer.CompanyLogoThumbUrl,
                            PublishedAt = offer.PublishedAt,
                            OpenToHireUkrainians = offer.OpenToHireUkrainians,
                            JobOfferRequiredSkills = new List<JobOfferRequiredSkills>(),
                            MultiLocation = offer.MultiLocation,
                            EmploymentTypes = offer.EmploymentTypes
                        };

                        if (offer.RequiredSkills == null)
                        {
                            offer.RequiredSkills = new List<string>();
                        }

                        foreach (var skillName in offer.RequiredSkills)
                        {
                            if (!existingSkills.TryGetValue(skillName, out var existingSkill))
                            {
                                existingSkill = new Skill { Name = skillName };
                                newSkills.Add(existingSkill);
                                existingSkills[skillName] = existingSkill;
                            }

                            newJobOffer.JobOfferRequiredSkills.Add(new JobOfferRequiredSkills
                            {
                                Skill = existingSkill
                            });
                        }

                        jobOffersToAdd.Add(newJobOffer);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                page++;
                Console.WriteLine($"Page {page} of {totalPages}.");
            } while (page < totalPages);
            
            if (newSkills.Count > 0)
            {
                dbContext.Skills.AddRange(newSkills);
                await dbContext.SaveChangesAsync();
            }

            if (jobOffersToAdd.Count > 0)
            {
                dbContext.JobOffers.AddRange(jobOffersToAdd);
            }
            await dbContext.SaveChangesAsync();
        }

        static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    "Server=baza-projektowa.postgres.database.azure.com;Database=jobhunter;Port=5432;User Id=postgres;Password=Baza@2025"));
        }
    }
}