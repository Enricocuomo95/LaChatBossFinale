using ChatCumStaj.Models;
using MongoDB.Bson;

namespace ChatCumStaj.Repository
{
    public interface IStanza
    {
        public bool InsertUserIntoChatRoom(string username, string codiceStanza);
        //qui prendo lo user creatore della stanza.
        //Egli non è un utente speciale, poichè tutti hanno permessi di admin
        //Ma per convenzione si è deciso che solo costui può cancellare la stanza
        //Inoltre l'entità stanza ha una relazione di composizione con utente
        public bool CreateRoom(Stanza room);
        public List<Stanza>? getStanzeForUser(string username);
        public Stanza? getById(ObjectId chatRoomId);
        public Stanza? getByCode(string cr_code);
        public List<string>? getUsersByStanza(string codiceStanza);
        public bool updateStanza(Stanza stanza);
        public bool DeleteRoom(Stanza room, string username);
        public string getCodeFromIdMessage(ObjectId id);
    }
}
