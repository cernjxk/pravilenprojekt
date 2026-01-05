using PravilenProjekt.Models;

namespace PravilenProjekt.Services
{
    public class PersonalLibraryService
    {
        public bool IsValidTitle(string? title)
            => !string.IsNullOrWhiteSpace(title);

        public bool IsValidRating(int? rating)
            => rating is null || (rating >= 1 && rating <= 10);

        public PersonalGame CreateGame(string title, string? genre, int? rating)
        {
            if (!IsValidTitle(title)) throw new ArgumentException("Title is required.");
            if (!IsValidRating(rating)) throw new ArgumentException("Rating must be 1–10 or null.");

            return new PersonalGame
            {
                Id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                Title = title.Trim(),
                Genre = string.IsNullOrWhiteSpace(genre) ? null : genre.Trim(),
                Rating = rating
            };
        }
    }
}
