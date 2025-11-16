using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PravilenProjekt.Models;
using PravilenProjekt.Services;

namespace PravilenProjekt.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly GameService _gameService;

        [BindProperty]
        public Game Game { get; set; } = new();

        public DeleteModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult OnGet(int id)
        {
            var game = _gameService.GetGameById(id);

            if (game == null)
            {
                return NotFound();
            }

            Game = game;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var game = _gameService.GetGameById(id);

            if (game != null)
            {
                _gameService.DeleteGame(id);
                TempData["SuccessMessage"] = $"Igra '{game.Title}' je bila uspešno izbrisana!";
            }

            return RedirectToPage("./Index");
        }
    }
}

