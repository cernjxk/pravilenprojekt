using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PravilenProjekt.Models;
using PravilenProjekt.Services;

namespace PravilenProjekt.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GameService _gameService;
        public List<Game> Games { get; set; } = new();
        public List<string> AvailableGenres { get; set; } = new();
        public List<string> AvailablePlatforms { get; set; } = new();
        
        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? GenreFilter { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string? PlatformFilter { get; set; }

        public IndexModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public void OnGet()
        {
            var allGames = _gameService.GetAllGames();

            // Pridobi vse unikatne žanre in platforme za dropdown
            AvailableGenres = allGames.Select(g => g.Genre).Distinct().OrderBy(g => g).ToList();
            AvailablePlatforms = allGames.Select(g => g.Platform).Distinct().OrderBy(p => p).ToList();

            // Filtriraj igre
            var filteredGames = allGames.AsEnumerable();

            // Search po naslovu
            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                filteredGames = filteredGames.Where(g => 
                    g.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            // Filter po žanru
            if (!string.IsNullOrWhiteSpace(GenreFilter))
            {
                filteredGames = filteredGames.Where(g => g.Genre == GenreFilter);
            }

            // Filter po platformi
            if (!string.IsNullOrWhiteSpace(PlatformFilter))
            {
                filteredGames = filteredGames.Where(g => g.Platform == PlatformFilter);
            }

            Games = filteredGames.ToList();
        }

        public bool HasActiveFilters()
        {
            return !string.IsNullOrWhiteSpace(SearchQuery) || 
                   !string.IsNullOrWhiteSpace(GenreFilter) || 
                   !string.IsNullOrWhiteSpace(PlatformFilter);
        }
    }
}