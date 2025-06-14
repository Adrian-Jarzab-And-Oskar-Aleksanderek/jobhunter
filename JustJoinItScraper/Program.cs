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
            client.DefaultRequestHeaders.Add("path", "/v2/user-panel/offers?&page=2&sortBy=published&orderBy=DESC&perPage=100&salaryCurrencies=PLN");
            client.DefaultRequestHeaders.Add("scheme", "https");
            client.DefaultRequestHeaders.Add("accept", "application/json, text/plain, */*");
            client.DefaultRequestHeaders.Add("accept-encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("accept-language", "pl-PL,pl;q=0.9,en-US;q=0.8,en;q=0.7");
            client.DefaultRequestHeaders.Add("origin", "https://justjoin.it");
            client.DefaultRequestHeaders.Add("priority", "u=1, i");
            client.DefaultRequestHeaders.Add("referer", "https://justjoin.it/");
            client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Chromium\";v=\"130\", \"Google Chrome\";v=\"130\", \"Not?A_Brand\";v=\"99\"");
            client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
            client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
            client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
            client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
            client.DefaultRequestHeaders.Add("sec-fetch-site", "same-site");
            client.DefaultRequestHeaders.Add("version", "2");
            client.DefaultRequestHeaders.Add("x-ga", "GA1.1.366351086.1728371165");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");
            
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var api = new API_GET(client);
            int page = 1, totalPages = 0;
            
            var existingSkills = dbContext.Skills
                .GroupBy(s => s.Name.ToLower())
                .ToDictionary(g => g.Key, g => g.First());
            var existingCompanies = dbContext.Companies.ToDictionary(c => c.Name, c => c);
            var skillCategories = new Dictionary<string, SkillCategory>();
            
            var categoryNames = new[]
            {
                "Programming Languages",
                "Databases",
                "Cloud Platforms",
                "Web Frameworks",
                "DevOps",
                "Testing",
                "Tools",
                "Mobile Development",
                "AI/ML",
                "Game Development",
                "Blockchain",
                "Security",
                "Agile & Project Management",
                "SAP",
                "Others"
            };

            foreach (var categoryName in categoryNames)
            {
                var category = dbContext.SkillCategories.FirstOrDefault(c => c.Name == categoryName);
                if (category == null)
                {
                    category = new SkillCategory { Name = categoryName, Description = $"{categoryName} skills" };
                    dbContext.SkillCategories.Add(category);
                }
                skillCategories[categoryName] = category;
            }
            await dbContext.SaveChangesAsync();

            var newSkills = new List<Skill>();
            var newCompanies = new List<Company>();
            var jobOffersToAdd = new List<JobOffer>();
            var requiredSkillsToAdd = new List<RequierdSkills>();
            var niceToHaveSkillsToAdd = new List<NiceToHaveSkills>();
            
            foreach (var skill in SkillReference.GetReferenceSkills())
            {
                SkillSimilarityDetector.AddSkill(skill.Name);
                foreach (var alias in skill.Aliases)
                { 
                    SkillSimilarityDetector.AddSkill(alias);
                }
            }

            do
            {
                string url = $"https://api.justjoin.it/v2/user-panel/offers?page={page}&sortBy=published&orderBy=DESC&perPage=100&salaryCurrencies=PLN";
                try
                {
                    var response = await api.GetResponseAsync(url);
                    var content = await api.DecodeResponseContentAsync(response);

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        break;
                    }

                    var jobOffers = JsonConvert.DeserializeObject<JobOfferResponse>(content);
                    if (jobOffers?.Meta == null || jobOffers.Data == null || !jobOffers.Data.Any())
                    {
                        break;
                    }

                    totalPages = jobOffers.Meta.TotalPages;

                    var firstOffer = jobOffers.Data.First();

                    foreach (var offer in jobOffers.Data)
                    {
                        Company company;
                        if (!existingCompanies.TryGetValue(offer.CompanyName, out company))
                        {
                            company = new Company
                            {
                                Name = offer.CompanyName,
                                LogoUrl = offer.CompanyLogoThumbUrl,
                                Description = string.Empty 
                            };
                            newCompanies.Add(company);
                            existingCompanies[offer.CompanyName] = company;
                        }

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
                            Company = company,
                            CompanyId = company.Id,
                            CompanyLogoThumbUrl = offer.CompanyLogoThumbUrl,
                            PublishedAt = offer.PublishedAt,
                            OpenToHireUkrainians = offer.OpenToHireUkrainians,
                            MultiLocation = offer.MultiLocation?.Select(ml => new MultiLocation
                            {
                                City = ml.City,
                                Slug = ml.Slug,
                                Street = ml.Street,
                                Latitude = ml.Latitude,
                                Longitude = ml.Longitude
                            }).ToList() ?? new List<MultiLocation>(),
                            EmploymentTypes = offer.EmploymentTypes?.Select(et => new EmploymentType
                            {
                                To = et.To,
                                From = et.From,
                                Type = et.Type,
                                Gross = et.Gross,
                                To_Chf = et.To_Chf,
                                To_Eur = et.To_Eur,
                                To_Gbp = et.To_Gbp,
                                To_Pln = et.To_Pln,
                                To_usd = et.To_usd,
                                Currency = et.Currency,
                                From_Chf = et.From_Chf,
                                From_Eur = et.From_Eur,
                                From_Gbp = et.From_Gbp,
                                From_Pln = et.From_Pln,
                                From_Usd = et.From_Usd
                            }).ToList() ?? new List<EmploymentType>()
                        };

                        if (offer.RequiredSkills != null && offer.RequiredSkills.Any())
                        {
                            var requiredSkillsList = new List<Skill>();
                            foreach (var skillName in offer.RequiredSkills)
                            {
                                try
                                {
                                    var normalizedSkillName = skillName.ToLower().Trim();
                                    if (!existingSkills.TryGetValue(normalizedSkillName, out var existingSkill))
                                    {
                                        var referenceSkill = SkillReference.GetReferenceSkills()
                                            .FirstOrDefault(s => 
                                                SkillSimilarityDetector.CalculateSimilarity(normalizedSkillName, s.Name.ToLower().Trim()) >= 0.8 ||
                                                s.Aliases.Any(a => SkillSimilarityDetector.CalculateSimilarity(normalizedSkillName, a.ToLower().Trim()) >= 0.8));

                                        var category = referenceSkill != null && skillCategories.ContainsKey(referenceSkill.Category)
                                            ? skillCategories[referenceSkill.Category]
                                            : skillCategories["Others"];

                                        existingSkill = new Skill 
                                        { 
                                            Name = normalizedSkillName,
                                            Category = category
                                        };
                                        newSkills.Add(existingSkill);
                                        existingSkills[normalizedSkillName] = existingSkill;
                                        SkillSimilarityDetector.AddSkill(normalizedSkillName);
                                    }
                                    requiredSkillsList.Add(existingSkill);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error processing required skill {skillName}: {ex.Message}");
                                }
                            }
                            requiredSkillsToAdd.Add(new RequierdSkills { Skills = requiredSkillsList });
                        }

                        if (offer.NiceToHaveSkills != null && offer.NiceToHaveSkills.Any())
                        {
                            var niceToHaveSkillsList = new List<Skill>();
                            foreach (var skillName in offer.NiceToHaveSkills)
                            {
                                try
                                {
                                    var normalizedSkillName = skillName.ToLower().Trim();
                                    if (!existingSkills.TryGetValue(normalizedSkillName, out var existingSkill))
                                    {
                                        var referenceSkill = SkillReference.GetReferenceSkills()
                                            .FirstOrDefault(s => 
                                                SkillSimilarityDetector.CalculateSimilarity(normalizedSkillName, s.Name.ToLower().Trim()) >= 0.8 ||
                                                s.Aliases.Any(a => SkillSimilarityDetector.CalculateSimilarity(normalizedSkillName, a.ToLower().Trim()) >= 0.8));

                                        var category = referenceSkill != null && skillCategories.ContainsKey(referenceSkill.Category)
                                            ? skillCategories[referenceSkill.Category]
                                            : skillCategories["Others"];

                                        existingSkill = new Skill 
                                        { 
                                            Name = normalizedSkillName,
                                            Category = category
                                        };
                                        newSkills.Add(existingSkill);
                                        existingSkills[normalizedSkillName] = existingSkill;
                                        SkillSimilarityDetector.AddSkill(normalizedSkillName);
                                    }
                                    niceToHaveSkillsList.Add(existingSkill);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"Error processing nice to have skill {skillName}: {ex.Message}");
                                }
                            }
                            Console.WriteLine($"[PROCESSED] Offer: {offer.Title}, NiceToHaveSkills: {string.Join(", ", niceToHaveSkillsList.Select(s => s.Name))}");
                            niceToHaveSkillsToAdd.Add(new NiceToHaveSkills { Skills = niceToHaveSkillsList });
                        }

                        jobOffersToAdd.Add(newJobOffer);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                page++;
            } while (page < totalPages);
            
            if (newSkills.Any())
            {
                dbContext.Skills.AddRange(newSkills);
                await dbContext.SaveChangesAsync();
            }

            if (newCompanies.Any())
            {
                dbContext.Companies.AddRange(newCompanies);
                await dbContext.SaveChangesAsync();
            }

            if (jobOffersToAdd.Any())
            {
                dbContext.JobOffers.AddRange(jobOffersToAdd);
                await dbContext.SaveChangesAsync();

                for (int i = 0; i < jobOffersToAdd.Count; i++)
                {
                    if (i < requiredSkillsToAdd.Count)
                    {
                        requiredSkillsToAdd[i].JobOfferId = jobOffersToAdd[i].Id;
                    }
                    if (i < niceToHaveSkillsToAdd.Count)
                    {
                        niceToHaveSkillsToAdd[i].JobOfferId = jobOffersToAdd[i].Id;
                    }
                }

                dbContext.RequierdSkills.AddRange(requiredSkillsToAdd);
                dbContext.NiceToHaveSkills.AddRange(niceToHaveSkillsToAdd);
                await dbContext.SaveChangesAsync();
            }

            SkillSimilarityDetector.SaveSimilarities();
        }

        static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql("Server=baza-projektowa.postgres.database.azure.com;Database=jobhunter;Port=5432;User Id=postgres;Password=Baza@2025"));
        }
    }
}