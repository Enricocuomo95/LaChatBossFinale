using ChatCumStaj.DTO;
using ChatCumStaj.Models;
using ChatCumStaj.Repository;
using MongoDB.Bson;

namespace ChatCumStaj.Service
{
    public class MessaggioService : IMessaggioService
    {
        private readonly IMessaggio messaggioRepo;
        private readonly StanzaService stanzaService;
        public MessaggioService(IMessaggio messaggioRepo, StanzaService stanzaService) 
        {
            this.messaggioRepo = messaggioRepo;
            this.stanzaService = stanzaService;
        }
        public bool CreateMessage(MessaggioDto message)
        {
            if ((message.StanzaCode == null) || (message.StanzaCode.Equals("")))
                return (false);

            Stanza s = this.stanzaService.getStanzaByCode(message.StanzaCode);
            if (s == null)
                return (false);


            Messaggio m = this.messaggioRepo.CreateMessage(convertToMessage(message));
            s.MessageRIF.Add(m.MessageId);
            if(this.stanzaService.updateStanza(s))
                return (true);

            return (false);
        }

        public MessaggioDto getMessageForId(ObjectId messaggioId)
        {
            return(convertToDto(
                this.messaggioRepo.getMessagesForId(messaggioId)
                ));
        }

        public List<MessaggioDto> getMessagesForStanzaCode(string stanzaCode)
        {
            Stanza s = this.stanzaService.getStanzaByCode(stanzaCode);
            List<MessaggioDto> risultato = new List<MessaggioDto>();
            s.MessageRIF.ForEach(element =>
            {
                risultato.Add(this.getMessageForId(element));
            });

            if (risultato.Count > 0)
                return (risultato);
            return (null);
        }

        public MessaggioDto convertToDto(Messaggio x)
        {
            return (new MessaggioDto()
            {
                Date = x.Date,
                Dati = x.Dati,
                UsernameMittente = x.UsernameMittente,
                StanzaCode = this.stanzaService.getCodeFromIdMessage(x.MessageId)
            });
        }

        public Messaggio convertToMessage(MessaggioDto x)
        {
            return (new Messaggio()
            {
                Date = x.Date,
                Dati = x.Dati,
                UsernameMittente = x.UsernameMittente
            });
        }
    }
}
