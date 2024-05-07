
using ChatCumStaj.Models;

namespace ChatCumStaj.Repository
{
    public class UtenteRepo : IRepo
    {
        private static ChatChattiamoContext _context;

        public UtenteRepo(ChatChattiamoContext context)
        {
            _context = context;
        }
        public bool DeleteUtente(Utente utente)
        {
            bool risposta = false;
            try
            {
                _context.Utentes.Remove(utente);
                _context.SaveChanges();
                risposta = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return (risposta);
        }

        public List<Utente> getListAll()
        {
            return (_context.Utentes.ToList());
        }

        public Utente getUtenteForUsername(string username)
        {
            return (_context.Utentes.FirstOrDefault(u => u.Username == username));
        }

        public bool InsertUtente(Utente utente)
        {
            bool risposta = false;
            try
            {
                _context.Utentes.Add(utente);
                _context.SaveChanges();
                risposta = true;

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return (risposta);
        }

        public bool UpdateUtente(Utente utente)
        {
            bool risposta = false;
            try
            {
                _context.Utentes.Update(utente);
                _context.SaveChanges();
                risposta = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return (risposta); ;
        }
    }
}
