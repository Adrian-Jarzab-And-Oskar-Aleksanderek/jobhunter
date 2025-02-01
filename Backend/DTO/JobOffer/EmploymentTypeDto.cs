namespace Backend.DTO.JobOffer;

public class EmploymentTypeDto
{
    public int Id { get; set; }
    public double? To { get; set; }
    public double? From { get; set; }
    public string Type { get; set; }
    public bool Gross { get; set; }
    public double? To_Chf { get; set; }
    public double? To_Eur { get; set; }
    public double? To_Gbp { get; set; }
    public double? To_Pln { get; set; }
    public double? To_usd { get; set; }
    public string Currency { get; set; }
    public double? From_Chf { get; set; }
    public double? From_Eur { get; set; }
    public double? From_Gbp { get; set; }
    public double? From_Pln { get; set; }
    public double? From_Usd { get; set; }
    public int JobOfferId { get; set; }
}