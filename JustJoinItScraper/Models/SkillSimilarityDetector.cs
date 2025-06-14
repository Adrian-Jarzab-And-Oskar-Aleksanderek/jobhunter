using System.Text.RegularExpressions;
using System.Text.Json;
using System.Net.Http;

namespace Scraper.Models
{
    public class SkillSimilarityDetector
    {
        private static readonly Dictionary<string, HashSet<string>> _similarSkills = new();
        private static readonly string _logFilePath = "skill_similarities.txt";
        private const double SIMILARITY_THRESHOLD = 0.8;

        static SkillSimilarityDetector()
        {
            SkillReference.LoadFromFile();
        }

        public static void AddSkill(string skillName)
        {
            if (string.IsNullOrEmpty(skillName))
                return;

            var normalized = Normalize(skillName);
            var existingGroup = FindSimilarGroup(normalized);
            
            if (existingGroup != null)
            {
                _similarSkills[existingGroup].Add(skillName);
            }
            else
            {
                _similarSkills[normalized] = new HashSet<string> { skillName };
            }
        }

        private static string? FindSimilarGroup(string normalizedSkill)
        {
            foreach (var group in _similarSkills.Keys)
            {
                if (CalculateSimilarity(normalizedSkill, group) >= SIMILARITY_THRESHOLD)
                {
                    return group;
                }
            }
            return null;
        }

        public static double CalculateSimilarity(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return 0;

            var normalized1 = Normalize(s1);
            var normalized2 = Normalize(s2);

            if (normalized1 == normalized2)
                return 1.0;


            int distance = LevenshteinDistance(normalized1, normalized2);
            int maxLength = Math.Max(normalized1.Length, normalized2.Length);
            
            return 1.0 - ((double)distance / maxLength);
        }

        private static int LevenshteinDistance(string s1, string s2)
        {
            int[,] d = new int[s1.Length + 1, s2.Length + 1];

            for (int i = 0; i <= s1.Length; i++)
                d[i, 0] = i;

            for (int j = 0; j <= s2.Length; j++)
                d[0, j] = j;

            for (int j = 1; j <= s2.Length; j++)
            {
                for (int i = 1; i <= s1.Length; i++)
                {
                    if (s1[i - 1] == s2[j - 1])
                        d[i, j] = d[i - 1, j - 1];
                    else
                        d[i, j] = Math.Min(d[i - 1, j] + 1, Math.Min(d[i, j - 1] + 1, d[i - 1, j - 1] + 1));
                }
            }

            return d[s1.Length, s2.Length];
        }

        public static void SaveSimilarities()
        {
            var lines = _similarSkills
                .Where(kvp => kvp.Value.Count > 1)
                .Select(kvp => $"{kvp.Key}: {string.Join(", ", kvp.Value)}")
                .ToList();

            if (lines.Any())
            {
                File.WriteAllLines(_logFilePath, lines);
            }
        }

        private static string Normalize(string skillName)
        {
            if (string.IsNullOrEmpty(skillName))
                return skillName;

            return skillName.ToLower().Trim();
        }
    }
} 