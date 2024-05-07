using ChatCumStaj.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace ChatCumStaj.Repository
{
    public class StanzaRepo: IStanza
    {
        private IMongoCollection<Stanza> stanze;
        private readonly ILogger _logger;

        public StanzaRepo(IConfiguration configuration, ILogger<StanzaRepo> logger)
        {
            this._logger = logger;

            string? connessioneLocale = configuration.GetValue<string>("MongoDbSettings:Locale");
            string? databaseName = configuration.GetValue<string>("MongoDbSettings:DatabaseName");

            MongoClient client = new MongoClient(connessioneLocale);
            IMongoDatabase _database = client.GetDatabase(databaseName);
            this.stanze = _database.GetCollection<Stanza>("Stanze");
        }

        public bool InsertUserIntoChatRoom(string username, string codiceStanza)
        {
            try
            {
               Stanza? s = (Stanza)this.stanze.Find(s=> s.StanzaCode == codiceStanza);
                if (s == null)
                    return (false);
                var membro = from Utente in s.Membri
                             where Utente == username
                             select Utente;

                if (membro != null)
                    return (false);
                s.Membri.Add(username);
                _logger.LogInformation("Inserimento effettuato con successo");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }


        public bool CreateRoom(Stanza room)
        {
            try
            {
                if (stanze.Find(s => s.StanzaId == room.StanzaId).ToList().Count > 0)
                    return false;

                stanze.InsertOne(room);
                _logger.LogInformation("Inserimento effettuato con successo");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }

        public List<Stanza>? getStanzeForUser(string username)
        {
            try
            {
                return stanze.Find(s => s.Membri.Contains(username)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public Stanza? getById(ObjectId chatRoomId)
        {
            try
            {
                return(stanze.Find(s=> s.StanzaId == chatRoomId).ToList()[0]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public Stanza? getByCode(string cr_code)
        {
            try
            {
                return (stanze.Find(s => s.StanzaCode == cr_code).ToList()[0]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public List<string>? getUsersByStanza(string codiceStanza)
        {
            Stanza s = this.getByCode(codiceStanza);
            if (s == null) 
                return null;
            return (s.Membri);
        }

        public bool DeleteRoom(Stanza room, string username)
        {
            try
            {
                Stanza s = this.getByCode(room.StanzaCode);
                if (s == null)
                    return false;

                if (s.Membri[0] == username)
                    stanze.DeleteOne<Stanza>(s => s.StanzaId == room.StanzaId);

                _logger.LogInformation("Eliminazione effettuata con successo");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return false;
        }

        public bool updateStanza(Stanza item)
        {
            Stanza? temp = getByCode(item.StanzaCode);
            if (temp != null)
            {
                temp.Title = item.Title != null ? item.Title : temp.Title;
                temp.Descrizione = item.Descrizione != null ? item.Descrizione : temp.Descrizione;
                temp.Membri = item.Membri.Count > 0 ? item.Membri : temp.Membri;
                temp.MessageRIF = item.MessageRIF.Count > 0 ? item.MessageRIF : temp.MessageRIF;

                var filter = Builders<Stanza>.Filter.Eq(s => s.StanzaCode, temp.StanzaCode);
                try
                {
                    stanze.ReplaceOne(filter, temp);
                    return true;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }

            return false;
        }


        public string getCodeFromIdMessage(ObjectId id)
        {
            try
            {
                return stanze.Find(s => s.MessageRIF.Contains(id)).ToList()[0].StanzaCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

    }
}

