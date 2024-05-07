using ChatCumStaj.Models;

namespace ChatCumStaj.Service
{
    public interface IService
    {
        public bool InsertUtente(Utente utenteDto);
        public Utente getUtenteForUsername(string username);
        public List<string> getListAll();
        public bool UpdateUtente(Utente utenteDto);
        public bool DeleteUtente(Utente utenteDto);
        public bool LoggaUtente(Utente utenteDto);

    }
}
