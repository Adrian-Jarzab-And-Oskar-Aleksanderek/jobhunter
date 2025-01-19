using Backend.DTO;
using Backend.Models;

namespace Backend.Mappers
{
    public static class MultiLocationMapper
    {
        public static MultiLocationDto MapMultiLocationDto(this MultiLocation multiLocation)
        {
            return new MultiLocationDto
            {
                City = multiLocation.City,
                Slug = multiLocation.Slug,
                Street = multiLocation.Street
            };
        }
    }
}