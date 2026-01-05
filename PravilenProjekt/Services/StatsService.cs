using PravilenProjekt.Models;

namespace PravilenProjekt.Services
{
    public class StatsService
    {
        public int TotalGames(IEnumerable<PersonalGame> games)
            => games?.Count() ?? 0;

        public double AverageRating(IEnumerable<PersonalGame> games)
        {
            if (games == null) return 0;

            var ratings = games.Where(g => g.Rating.HasValue).Select(g => g.Rating!.Value).ToList();
            if (ratings.Count == 0) return 0;

            return ratings.Average();
        }

        public string MostCommonGenre(IEnumerable<PersonalGame> games)
        {
            if (games == null) return "Ni podatkov";

            var genres = games
                .Select(g => g.Genre?.Trim())
                .Where(g => !string.IsNullOrWhiteSpace(g))
                .Select(g => g!)
                .ToList();

            if (genres.Count == 0) return "Ni podatkov";

            return genres
                .GroupBy(g => g, StringComparer.OrdinalIgnoreCase)
                .OrderByDescending(grp => grp.Count())
                .ThenBy(grp => grp.Key)
                .First().Key;
        }
    }
}
