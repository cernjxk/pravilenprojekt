using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PravilenProjekt.Models;
using PravilenProjekt.Services;

namespace PravilenProjekt.Pages
{
    public class CreateModel : PageModel
    {
        private readonly GameService _gameService;

        [BindProperty]
        public Game Game { get; set; } = new();

        public CreateModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public void OnGet()
        {
            // Prazna stran za vnos
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _gameService.AddGame(Game);
            TempData["SuccessMessage"] = $"Igra '{Game.Title}' je bila uspešno dodana!";

            return RedirectToPage("./Index");
        }
    }
}
