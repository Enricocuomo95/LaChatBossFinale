using ChatCumStaj.DTO;
using ChatCumStaj.Models;
using ChatCumStaj.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChatCumStaj.Controllers
{
    [ApiController]
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly IService utenteService;
        private readonly IStanzaService stanzaService;
        private IMessaggioService messaggioService;

        public HomeController(IService utenteService, IStanzaService stanzaService, IMessaggioService messaggioService)
        {
            this.utenteService = utenteService;
            this.stanzaService = stanzaService;
            this.messaggioService = messaggioService;
        }
        private bool controllaUtente(Utente utenteDto)
        {
            if (utenteDto.Username is null || utenteDto.Username.Trim().Equals(""))
                return false;
            if (utenteDto.Passward is null || utenteDto.Passward.Trim().Equals(""))
                return false;
            return (true);
        }

        [HttpPost("inserisciUtente")]
        public IActionResult InserisciUtente(Utente utenteDto)
        {
            if(utenteDto == null)
                return BadRequest();

            if (!controllaUtente(utenteDto))
                return BadRequest();

            if (utenteService.InsertUtente(utenteDto))
                return Ok();
            return BadRequest();
        }

        [HttpPost("loggaUtente")]
        public IActionResult loggaUtente(Utente utenteDto)
        {
            if (utenteDto == null)
                return BadRequest();

            if (!controllaUtente(utenteDto))
                return BadRequest();

            if (utenteService.LoggaUtente(utenteDto))
                return Ok();
            return BadRequest();
        }


        [HttpPost("login")]
        public ActionResult getToken(Utente utenteDto)
        {
            List<Claim> claims = new List<Claim>()
            {
                //claim è standard mvc
                new Claim(JwtRegisteredClaimNames.Sub, utenteDto.Username),
                //il claim è il contenuto key-value salvato nel playload del token
                new Claim("UserType", "ADMIN"),
                //evito che due token abbiano lo stesso JWT
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            //ci basiamo su una chiave simmetrica unica per cifrare e decifrare.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key_your_super_secret_key"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "ChatCumStaj",
                audience: "Popolo",
                claims: claims,          //Body o Payload del JWT
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
                );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }


        [HttpPost("creaStanza")]
        public IActionResult CreaStanza(StanzaDto stanza)
        {
            if (this.stanzaService.CreateRoom(stanza))
                return (Ok());
            return BadRequest();
        }

        [HttpPost("updateStanza")]
        public IActionResult UpdateStanza(StanzaDto stanza)
        {
            if (this.stanzaService.updateStanza(stanza))
                return (Ok());
            return BadRequest();
        }


        [HttpPost("creaMessaggio")]
        public IActionResult CreaMessaggio(MessaggioDto messaggio)
        {
            if (this.messaggioService.CreateMessage(messaggio))
                return (Ok());
            return BadRequest();
        }


        [HttpGet("{username}")]
        public ActionResult<List<StanzaDto>> getStanze(string username)
        {
            return (this.stanzaService.getStanzeForUser(username));
        }


        [HttpPost("{stanzaCode}")]
        public ActionResult<List<MessaggioDto>> getMessagesForStanzaCode(string stanzaCode)
        {
            return (this.messaggioService.getMessagesForStanzaCode(stanzaCode));
        }
      

        [HttpGet("utenti")]
        public ActionResult<List<string>> getUtenti()
        {
            return (this.utenteService.getListAll());
        }

    }
}
