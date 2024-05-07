namespace ChatCumStaj.DTO
{
    public class StanzaDto
    {
        public string StanzaCode { get; set; }
        public string? Title { get; set; }
        public string? Descrizione { get; set; }
        public List<string> Membri { get; set; } = new List<string>();
    }
}
