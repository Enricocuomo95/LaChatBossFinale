using ChatCumStaj.Models;
using MongoDB.Bson;

namespace ChatCumStaj.Repository
{
    public interface IMessaggio
    {
        public Messaggio CreateMessage(Messaggio message);
        public Messaggio getMessagesForId(ObjectId messaggioId);
    }
}
