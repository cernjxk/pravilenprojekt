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

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        public IndexModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public void OnGet()
        {
            var allGames = _gameService.GetAllGames();

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                Games = allGames
                    .Where(g => g.Title.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else
            {
                Games = allGames;
            }
        }
    }
}