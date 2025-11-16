using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PravilenProjekt.Models;

namespace PravilenProjekt.Pages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Game Game { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            
            var games = GetAllGames();
            var game = games.FirstOrDefault(g => g.Id == id);

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

            // TODO: Shranite v bazo
            // Za testiranje samo redirectamo nazaj
            TempData["SuccessMessage"] = $"Igra '{Game.Title}' je bila uspešno posodobljena!";

            return RedirectToPage("./Index");
        }

        private List<Game> GetAllGames()
        {
            // Isti testni podatki kot v Index.cshtml.cs
            return new List<Game>
            {
                new Game
                {
                    Id = 1,
                    Title = "The Legend of Zelda: Breath of the Wild",
                    Genre = "Akcijska avantura",
                    Platform = "Nintendo Switch",
                    Price = 59.99m,
                    ReleaseYear = 2017,
                    ImageUrl = "https://images.unsplash.com/photo-1612287230202-1ff1d85d1bdf?w=400&h=500&fit=crop",
                    Rating = 9.5
                },
                new Game
                {
                    Id = 2,
                    Title = "Elden Ring",
                    Genre = "RPG",
                    Platform = "Multi-platform",
                    Price = 59.99m,
                    ReleaseYear = 2022,
                    ImageUrl = "https://images.unsplash.com/photo-1542751371-adc38448a05e?w=400&h=500&fit=crop",
                    Rating = 9.3
                },
                new Game
                {
                    Id = 3,
                    Title = "Minecraft",
                    Genre = "Sandbox",
                    Platform = "Multi-platform",
                    Price = 26.95m,
                    ReleaseYear = 2011,
                    ImageUrl = "https://images.unsplash.com/photo-1579373903781-fd5c0c30c4cd?w=400&h=500&fit=crop",
                    Rating = 8.9
                }
            };
        }
    }
}
