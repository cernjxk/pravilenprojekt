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
                    ImageUrl = "https://m.media-amazon.com/images/I/81Ge3v6ro8L._AC_UF1000,1000_QL80_.jpg",
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
                    ImageUrl = "https://shared.fastly.steamstatic.com/store_item_assets/steam/apps/1245620/capsule_616x353.jpg?t=1748630546",
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
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/b/b6/Minecraft_2024_cover_art.png/250px-Minecraft_2024_cover_art.png",
                    Rating = 8.9
                }
            };
        }
    }
}
