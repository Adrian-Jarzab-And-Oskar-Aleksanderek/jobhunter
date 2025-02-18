using System.Text.Json.Serialization;

namespace Backend.DTO.JobOffer;

public class EmploymentTypeDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("to")]
    public double? To { get; set; }
    
    [JsonPropertyName("from")]
    public double? From { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("gross")]
    public bool Gross { get; set; }
    
    [JsonPropertyName("to_Chf")]
    public double? To_Chf { get; set; }

    [JsonPropertyName("to_Eur")]
    public double? To_Eur { get; set; }
    
    [JsonPropertyName("to_Gbp")]
    public double? To_Gbp { get; set; }
    
    [JsonPropertyName("to_Pln")]
    public double? To_Pln { get; set; }
    
    [JsonPropertyName("to_Usd")]
    public double? To_usd { get; set; }
    
    [JsonPropertyName("currency")]
    public string Currency { get; set; }
    
    [JsonPropertyName("from_Chf")]
    public double? From_Chf { get; set; }
    
    [JsonPropertyName("from_Eur")]
    public double? From_Eur { get; set; }
    
    [JsonPropertyName("from_Gbp")]
    public double? From_Gbp { get; set; }
    
    [JsonPropertyName("from_Pln")]
    public double? From_Pln { get; set; }
    
    [JsonPropertyName("from_Uhf")]
    public double? From_Usd { get; set; }
    
    [JsonPropertyName("jobOfferId")]
    public int JobOfferId { get; set; }
}