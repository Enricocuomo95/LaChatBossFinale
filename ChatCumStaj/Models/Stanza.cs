using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ChatCumStaj.Models
{
    public class Stanza
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId StanzaId { get; set; }

        [BsonElement("StanzaCode")]
        public string StanzaCode { get; set; } = Guid.NewGuid().ToString().ToUpper();

        [BsonElement("Title")]
        public string? Title { get; set; }

        [BsonElement("Descrizione")]
        public string? Descrizione { get; set; }

        public List<string> Membri { get; set; } = new List<string>();

        [BsonRepresentation(BsonType.ObjectId)]
        public List<ObjectId> MessageRIF { get; set; } = new List<ObjectId>();
    }
}
