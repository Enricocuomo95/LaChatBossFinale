using ChatCumStaj.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChatCumStaj.Repository
{
    public class MessaggioRepo : IMessaggio
    {
        private IMongoCollection<Messaggio> messaggi;
        private readonly ILogger _logger;

        public MessaggioRepo(IConfiguration configuration, ILogger<MessaggioRepo> logger)
        {
            this._logger = logger;

            string? connessioneLocale = configuration.GetValue<string>("MongoDbSettings:Locale");
            string? databaseName = configuration.GetValue<string>("MongoDbSettings:DatabaseName");

            MongoClient client = new MongoClient(connessioneLocale);
            IMongoDatabase _database = client.GetDatabase(databaseName);
            this.messaggi = _database.GetCollection<Messaggio>("Messaggi");
        }

        public Messaggio CreateMessage(Messaggio message)
        {
            try
            {
                if (messaggi.Find(m => m.MessageId == message.MessageId).ToList().Count > 0)
                    return null;

                messaggi.InsertOne(message);
                _logger.LogInformation("Inserimento effettuato con successo");
                return message;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public Messaggio getMessagesForId(ObjectId messaggioId)
        {
            return (messaggi.Find(m => m.MessageId == messaggioId).ToList()[0]);
        }
    }
}
