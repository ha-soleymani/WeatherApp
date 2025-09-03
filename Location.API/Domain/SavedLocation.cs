using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Location.API.Domain
{
    public class SavedLocation
    {
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        
        public required string CityName { get; set; }
        
        public DateTime SavedAt { get; set; }
    }
}
