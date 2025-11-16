namespace PravilenProjekt.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int ReleaseYear { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public double Rating { get; set; }
    }
}
