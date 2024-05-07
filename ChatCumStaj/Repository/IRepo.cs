using ChatCumStaj.Models;
namespace ChatCumStaj.Repository
{
    public interface IRepo
    {
        public bool InsertUtente(Utente utente);
        public Utente getUtenteForUsername(string username);
        public List<Utente> getListAll();
        public bool UpdateUtente(Utente utente);
        public bool DeleteUtente(Utente utente);

    }
}
