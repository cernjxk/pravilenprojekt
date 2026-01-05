using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PravilenProjekt.Models;
using PravilenProjekt.Services;

namespace PravilenProjekt.Pages
{
    public class EditModel : PageModel
    {
        private readonly GameService _gameService;

        [BindProperty]
        public Game Game { get; set; } = new();

        public EditModel(GameService gameService)
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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _gameService.UpdateGame(Game);
            TempData["SuccessMessage"] = $"Igra '{Game.Title}' je bila uspesno posodobljena!";

            return RedirectToPage("./Index");
        }
    }
}
