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

        public IndexModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public void OnGet()
        {
            Games = _gameService.GetAllGames();
        }
    }
}

