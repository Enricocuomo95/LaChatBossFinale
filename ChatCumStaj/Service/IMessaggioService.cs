using ChatCumStaj.DTO;
using ChatCumStaj.Models;
using MongoDB.Bson;

namespace ChatCumStaj.Service
{
    public interface IMessaggioService
    {
        public bool CreateMessage(MessaggioDto message);
        public MessaggioDto getMessageForId(ObjectId messaggioId);
        public List<MessaggioDto> getMessagesForStanzaCode(string stanzaCode);
    }
}
