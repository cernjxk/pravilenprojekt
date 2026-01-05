using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PravilenProjekt.Models;
using PravilenProjekt.Services;
using System.Text;

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

        [BindProperty(SupportsGet = true)]
        public bool ShowTopRated { get; set; }

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

            // Top Rated filter - Ocena 9.0 ali več
            if (ShowTopRated)
            {
                filteredGames = filteredGames.Where(g => g.Rating >= 9.0);
            }

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

            Games = filteredGames.OrderByDescending(g => g.Rating).ToList();
        }

        public bool HasActiveFilters()
        {
            return !string.IsNullOrWhiteSpace(SearchQuery) || 
                   !string.IsNullOrWhiteSpace(GenreFilter) || 
                   !string.IsNullOrWhiteSpace(PlatformFilter) ||
                   ShowTopRated;
        }

        public int GetTopRatedCount()
        {
            return _gameService.GetAllGames().Count(g => g.Rating >= 9.0);
        }

        // Export vseh iger v CSV
        public IActionResult OnGetExport()
        {
            var allGames = _gameService.GetAllGames();
            
            var csv = new StringBuilder();
            
            // Header
            csv.AppendLine("ID,Naslov,Žanr,Platforma,Cena,Leto izdaje,Ocena,URL slike");
            
            // Rows
            foreach (var game in allGames)
            {
                csv.AppendLine($"{game.Id},\"{EscapeCsv(game.Title)}\",\"{EscapeCsv(game.Genre)}\",\"{EscapeCsv(game.Platform)}\",{game.Price},{game.ReleaseYear},{game.Rating},\"{EscapeCsv(game.ImageUrl)}\"");
            }
            
            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            var fileName = $"videoigre_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            
            return File(bytes, "text/csv", fileName);
        }

        // Export samo filtriranih iger v CSV
        public IActionResult OnGetExportFiltered(string? searchQuery, string? genreFilter, string? platformFilter, bool showTopRated = false)
        {
            var allGames = _gameService.GetAllGames();
            var filteredGames = allGames.AsEnumerable();

            // Apply same filters as OnGet
            if (showTopRated)
            {
                filteredGames = filteredGames.Where(g => g.Rating >= 9.0);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                filteredGames = filteredGames.Where(g => 
                    g.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(genreFilter))
            {
                filteredGames = filteredGames.Where(g => g.Genre == genreFilter);
            }

            if (!string.IsNullOrWhiteSpace(platformFilter))
            {
                filteredGames = filteredGames.Where(g => g.Platform == platformFilter);
            }

            var games = filteredGames.ToList();
            
            var csv = new StringBuilder();
            csv.AppendLine("ID,Naslov,Žanr,Platforma,Cena,Leto izdaje,Ocena,URL slike");
            
            foreach (var game in games)
            {
                csv.AppendLine($"{game.Id},\"{EscapeCsv(game.Title)}\",\"{EscapeCsv(game.Genre)}\",\"{EscapeCsv(game.Platform)}\",{game.Price},{game.ReleaseYear},{game.Rating},\"{EscapeCsv(game.ImageUrl)}\"");
            }
            
            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            var fileName = $"videoigre_filtered_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            
            return File(bytes, "text/csv", fileName);
        }

        private string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            
            // Escape double quotes
            return value.Replace("\"", "\"\"");
        }
    }
}