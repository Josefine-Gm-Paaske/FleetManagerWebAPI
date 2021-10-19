namespace FleetManager.WebAPI.Model
{
    /// <summary>
    /// Helper methods for mapping model objects to dto
    /// </summary>
    public static class DtoEntityHelpers
    {
        public static CarDto Map(this Car car)
        {
            if (car == null)
            {
                return null;
            }
            return new CarDto
            {
                Brand = car.Brand,
                Mileage = car.Mileage,
                Reserved = car.Reserved,
                Href = car.ExtractHref(),
                LocationHref = car.Location?.ExtractHref()
            };
        }

        public static Car Map(this CarDto dto)
        {
            return new Car()
            {
                Id = dto.ExtractId(),
                Brand = dto.Brand,
                Mileage = dto.Mileage,
                Reserved = dto.Reserved,
            };
        }

        public static LocationDto Map(this Location location)
        {
            return new LocationDto
            {
                Name = location.Name,
                Href = location.ExtractHref(),
            };
        }

        public static Location Map(this LocationDto location)
        {
            return new Location()
            {
                Id = location.ExtractId(),
                Name = location.Name,
            };
        }

        public static int? ExtractId(this BaseDto dto)
        {            
            return GetIdFromHref(dto.Href);
        }

        public static string ExtractHref(this BaseModel model)
        {
            return $@"/api/{model.GetType().Name.ToLower()}s/{model.Id}";
        }

        public static int? GetIdFromHref(string href)
        {
            if (href == null)
            {
                return null;
            }
            if (href == string.Empty)
            {
                return null;
            }
            return int.Parse(href[(href.LastIndexOf("/") + 1)..]);
        }
    }
}
