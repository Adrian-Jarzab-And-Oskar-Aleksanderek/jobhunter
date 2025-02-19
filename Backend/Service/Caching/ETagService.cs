using System.Security.Cryptography;
using System.Text;

namespace Backend.Service.Caching;

public class ETagService
{
    public static string GenerateETag(string data)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(data));
        return Convert.ToBase64String(hash);
    }

    public static bool IsETagValid(HttpRequest request, string generatedETag)
    {
        if (request.Headers.IfNoneMatch.Count > 0)
        {
            return request.Headers.IfNoneMatch.Contains(generatedETag);
        }
        return false;
    }
}