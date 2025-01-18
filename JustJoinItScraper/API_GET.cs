using System.IO.Compression;
using System.Text;

public class API_GET
{
    private readonly HttpClient _client;

    public API_GET(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<HttpResponseMessage> GetResponseAsync(string url)
    {
        try
        {
            var response = await _client.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
            }

            return response;
        }
        catch (Exception ex)
        {
            throw new HttpRequestException($"An error occurred while making the request: {ex.Message}", ex);
        }
    }

    public async Task<string> DecodeResponseContentAsync(HttpResponseMessage response)
    {
        var contentStream = await response.Content.ReadAsStreamAsync();

        if (response.Content.Headers.ContentEncoding.Contains("gzip"))
        {
            try
            {
                using (var decompressionStream = new GZipStream(contentStream, CompressionMode.Decompress))
                using (var reader = new StreamReader(decompressionStream, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Error while decompressing the response content (gzip).", ex);
            }
        }
        else if (response.Content.Headers.ContentEncoding.Contains("deflate"))
        {
            try
            {
                using (var decompressionStream = new DeflateStream(contentStream, CompressionMode.Decompress))
                using (var reader = new StreamReader(decompressionStream, Encoding.UTF8))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Error while decompressing the response content (deflate).", ex);
            }
        }
        else
        {
            using (var reader = new StreamReader(contentStream, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
