using ChatCumStaj.Models;
using ChatCumStaj.Repository;

namespace ChatCumStaj.Service
{
    public class UtenteService : IService
    {
        private readonly UtenteRepo utenteRepo;

        public UtenteService(UtenteRepo utenteRepo)
        {
            this.utenteRepo = utenteRepo;
        }
        public bool DeleteUtente(Utente utenteDto)
        {
            Utente utente = utenteRepo.getUtenteForUsername(utenteDto.Username);
            if (utente == null)
                return false;

            return(utenteRepo.DeleteUtente(utente));
        }

        public List<string> getListAll()
        {
            List<Utente> list = utenteRepo.getListAll(); 
            List<string> result = new List<string>();

            foreach (Utente item in list)
                result.Add(item.Username);           
            return(result);
        }

        public Utente getUtenteForUsername(string username)
        {
            return (utenteRepo.getUtenteForUsername(username));
        }

        public bool InsertUtente(Utente utenteDto)
        {
            utenteDto.Passward = BCrypt.Net.BCrypt.HashPassword(utenteDto.Passward);
            return (utenteRepo.InsertUtente(utenteDto));
        }

        public bool LoggaUtente(Utente utenteDto)
        {
            Utente u = utenteRepo.getUtenteForUsername(utenteDto.Username);

            if (u == null)
                return false;

            if (!BCrypt.Net.BCrypt.Verify(utenteDto.Passward, u.Passward))
                return false;
            return (true);
        }

        public bool UpdateUtente(Utente utenteDto)
        {
            Utente u = utenteRepo.getUtenteForUsername(utenteDto.Username);

            if(u == null) 
                return false;

            if (!BCrypt.Net.BCrypt.Verify(utenteDto.Passward, u.Passward))
                return false;

            return (utenteRepo.UpdateUtente(utenteDto));
        }
    }
}
