using ChatCumStaj.DTO;
using ChatCumStaj.Models;
using ChatCumStaj.Repository;
using MongoDB.Bson;

namespace ChatCumStaj.Service
{
    public class StanzaService : IStanzaService
    {
        private readonly IStanza stanzaRepo;

        public StanzaService(IStanza stanzaRepo)
        {
            this.stanzaRepo = stanzaRepo;
        }
        public bool CreateRoom(StanzaDto room)
        {
            return (stanzaRepo.CreateRoom(convertToStanza(room)));
        }

        public bool DeleteRoom(StanzaDto room, string username)
        {
            return (stanzaRepo.DeleteRoom(new Stanza() { StanzaCode = room.StanzaCode }, username));
        }

        public StanzaDto? getByCode(string cr_code)
        {
            return (this.convertToDTO(stanzaRepo.getByCode(cr_code)));
        }

        public Stanza? getStanzaByCode(string cr_code)
        {
            return (stanzaRepo.getByCode(cr_code));
        }

        public string getCodeFromIdMessage(ObjectId id)
        {
            return (this.stanzaRepo.getCodeFromIdMessage(id));
        }

        public List<StanzaDto>? getStanzeForUser(string username)
        {
            List<StanzaDto> listaRisposta = new List<StanzaDto>();
            this.stanzaRepo.getStanzeForUser(username).ForEach( s =>
            {
                listaRisposta.Add(convertToDTO(s));
            });
            return(listaRisposta);
        }

        public List<String> getUsersByStanza(string codiceStanza)
        {
            return(this.stanzaRepo.getUsersByStanza(codiceStanza));
        }

        public bool InsertUserIntoChatRoom(string username, string codiceStanza)
        {
            return(this.stanzaRepo.InsertUserIntoChatRoom(username, codiceStanza));
        }

        public bool updateStanza(StanzaDto room)
        {
            return (this.stanzaRepo.updateStanza(convertToStanza(room)));
        }

        public bool updateStanza(Stanza room)
        {
            return (this.stanzaRepo.updateStanza(room));
        }

        public StanzaDto convertToDTO(Stanza x)
        {
            return (new StanzaDto()
            {
                StanzaCode = x.StanzaCode,
                Descrizione = x.Descrizione,
                Title = x.Title,
                Membri = x.Membri
            });

        }

        public Stanza convertToStanza(StanzaDto x)
        {
            return(new Stanza()
            {
                StanzaCode = x.StanzaCode,
                Title = x.Title,
                Descrizione = x.Descrizione,
                Membri = x.Membri
            });
        }
    }
}
