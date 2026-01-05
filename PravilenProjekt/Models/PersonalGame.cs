namespace PravilenProjekt.Models
{
    public class PersonalGame
    {
        public long Id { get; set; }
        public string Title { get; set; } = "";
        public string? Genre { get; set; }
        public int? Rating { get; set; }
        public string? Website { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.Now;
    }
}
