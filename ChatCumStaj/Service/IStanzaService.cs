using ChatCumStaj.DTO;
using MongoDB.Bson;

namespace ChatCumStaj.Service
{
    public interface IStanzaService
    {
        public string getCodeFromIdMessage(ObjectId id);
        public bool CreateRoom(StanzaDto room);
        public List<StanzaDto>? getStanzeForUser(string username);
        public StanzaDto? getByCode(string cr_code);
        public List<String> getUsersByStanza(string codiceStanza);
        public bool updateStanza (StanzaDto stanza);
        public bool DeleteRoom(StanzaDto room, string username);
    }
}
