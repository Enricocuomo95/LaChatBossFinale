using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ChatCumStaj.DTO
{
    public class MessaggioDto
    {
        public string? Dati { get; set; }
        public DateTime Date { get; set; }
        public string UsernameMittente { get; set; }
        public string StanzaCode { get; set; }

    }
}
