using PravilenProjekt.Models;

namespace PravilenProjekt.Services
{
    public class GameService
    {
        private static List<Game> _games = new()
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

        public List<Game> GetAllGames()
        {
            return _games;
        }

        public Game? GetGameById(int id)
        {
            return _games.FirstOrDefault(g => g.Id == id);
        }

        public void UpdateGame(Game game)
        {
            var existing = _games.FirstOrDefault(g => g.Id == game.Id);
            if (existing != null)
            {
                existing.Title = game.Title;
                existing.Genre = game.Genre;
                existing.Platform = game.Platform;
                existing.Price = game.Price;
                existing.ReleaseYear = game.ReleaseYear;
                existing.ImageUrl = game.ImageUrl;
                existing.Rating = game.Rating;
            }
        }

        public bool DeleteGame(int id)
        {
            var game = _games.FirstOrDefault(g => g.Id == id);
            if (game != null)
            {
                _games.Remove(game);
                return true;
            }
            return false;
        }

        public void AddGame(Game game)
        {
            game.Id = _games.Any() ? _games.Max(g => g.Id) + 1 : 1;
            _games.Add(game);
        }
    }
}

