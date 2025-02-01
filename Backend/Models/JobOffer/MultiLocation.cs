using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.JobOffer
{
    public class MultiLocation
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Slug { get; set; }
        public string Street { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [ForeignKey("JobOffer")]
        public int JobOfferId { get; set; }
        public JobOffer JobOffer { get; set; }
    }
}