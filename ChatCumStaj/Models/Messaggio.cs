using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ChatCumStaj.Models
{
    public class Messaggio
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId MessageId { get; set; }

        [BsonElement("data")]
        public string? Dati { get; set; }

        [BsonElement("date")]
        public DateTime Date { get; set; } = DateTime.Now;

        [BsonElement]
        public string UsernameMittente { get; set; } = null!;
    }
}
