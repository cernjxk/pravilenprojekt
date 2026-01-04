using Microsoft.VisualStudio.TestTools.UnitTesting;
using PravilenProjekt.Models;
using PravilenProjekt.Services;
using System.Linq;

namespace PravilenProjekt.Tests
{
    [TestClass]
    public class GameServiceTests
    {
        private GameService _gameService = null!;

        [TestInitialize]
        public void Setup()
        {
            _gameService = new GameService();
        }

        // Test 1: Pridobi vse igre
        [TestMethod]
        public void Test_GetAllGames_ReturnsListOfGames()
        {
            // Act
            var games = _gameService.GetAllGames();

            // Assert
            Assert.IsNotNull(games);
            Assert.IsTrue(games.Count >= 3, "Mora vsebovati vsaj 3 privzete igre");
        }

        // Test 2: Dodaj novo igro
        [TestMethod]
        public void Test_AddGame_AddsGameSuccessfully()
        {
            // Arrange
            var initialCount = _gameService.GetAllGames().Count;
            var newGame = new Game
            {
                Title = "Test Game",
                Genre = "Test Genre",
                Platform = "PC",
                Price = 29.99m,
                ReleaseYear = 2024,
                ImageUrl = "https://example.com/image.jpg",
                Rating = 8.5
            };

            // Act
            _gameService.AddGame(newGame);
            var games = _gameService.GetAllGames();

            // Assert
            Assert.AreEqual(initialCount + 1, games.Count);
            Assert.IsTrue(newGame.Id > 0, "Igra mora dobiti ID");
            Assert.IsTrue(games.Any(g => g.Title == "Test Game"));
        }

        // Test 3: Dodaj igro z avtomatskim ID
        [TestMethod]
        public void Test_AddGame_AssignsUniqueId()
        {
            // Arrange
            var game1 = new Game { Title = "Game 1", Genre = "Action", Platform = "PC", Price = 10, ReleaseYear = 2020, ImageUrl = "url1", Rating = 7.0 };
            var game2 = new Game { Title = "Game 2", Genre = "RPG", Platform = "PS5", Price = 20, ReleaseYear = 2021, ImageUrl = "url2", Rating = 8.0 };

            // Act
            _gameService.AddGame(game1);
            _gameService.AddGame(game2);

            // Assert
            Assert.AreNotEqual(game1.Id, game2.Id, "ID-ji morajo biti unikatni");
            Assert.IsTrue(game2.Id > game1.Id, "ID mora biti večji od prejšnjega");
        }

        // Test 4: Pridobi igro po ID
        [TestMethod]
        public void Test_GetGameById_ReturnsCorrectGame()
        {
            // Arrange
            var newGame = new Game
            {
                Title = "Specific Game",
                Genre = "Adventure",
                Platform = "Xbox",
                Price = 39.99m,
                ReleaseYear = 2023,
                ImageUrl = "https://example.com/specific.jpg",
                Rating = 9.0
            };
            _gameService.AddGame(newGame);

            // Act
            var retrievedGame = _gameService.GetGameById(newGame.Id);

            // Assert
            Assert.IsNotNull(retrievedGame);
            Assert.AreEqual("Specific Game", retrievedGame.Title);
            Assert.AreEqual(newGame.Id, retrievedGame.Id);
        }

        // Test 5: Pridobi neobstoječo igro
        [TestMethod]
        public void Test_GetGameById_ReturnsNullForNonExistentId()
        {
            // Act
            var game = _gameService.GetGameById(99999);

            // Assert
            Assert.IsNull(game);
        }

        // Test 6: Posodobi igro
        [TestMethod]
        public void Test_UpdateGame_UpdatesGameSuccessfully()
        {
            // Arrange
            var games = _gameService.GetAllGames();
            var gameToUpdate = games.First();
            var originalTitle = gameToUpdate.Title;

            var updatedGame = new Game
            {
                Id = gameToUpdate.Id,
                Title = "Updated Title",
                Genre = gameToUpdate.Genre,
                Platform = gameToUpdate.Platform,
                Price = 49.99m,
                ReleaseYear = gameToUpdate.ReleaseYear,
                ImageUrl = gameToUpdate.ImageUrl,
                Rating = 9.8
            };

            // Act
            _gameService.UpdateGame(updatedGame);
            var result = _gameService.GetGameById(gameToUpdate.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated Title", result.Title);
            Assert.AreEqual(49.99m, result.Price);
            Assert.AreEqual(9.8, result.Rating);
            Assert.AreNotEqual(originalTitle, result.Title);
        }

        // Test 7: Posodobi neobstoječo igro (ne sme crashati)
        [TestMethod]
        public void Test_UpdateGame_DoesNotCrashForNonExistentGame()
        {
            // Arrange
            var nonExistentGame = new Game
            {
                Id = 99999,
                Title = "Non Existent",
                Genre = "Test",
                Platform = "Test",
                Price = 0,
                ReleaseYear = 2000,
                ImageUrl = "test",
                Rating = 0
            };

            // Act & Assert (ne sme vreči izjeme)
            _gameService.UpdateGame(nonExistentGame);
            var game = _gameService.GetGameById(99999);
            Assert.IsNull(game, "Igra ne bi smela obstajati");
        }

        // Test 8: Izbriši igro
        [TestMethod]
        public void Test_DeleteGame_RemovesGameSuccessfully()
        {
            // Arrange
            var initialCount = _gameService.GetAllGames().Count;
            var newGame = new Game
            {
                Title = "Game To Delete",
                Genre = "Test",
                Platform = "PC",
                Price = 10,
                ReleaseYear = 2020,
                ImageUrl = "url",
                Rating = 5.0
            };
            _gameService.AddGame(newGame);

            // Act
            var deleteResult = _gameService.DeleteGame(newGame.Id);
            var games = _gameService.GetAllGames();

            // Assert
            Assert.IsTrue(deleteResult, "Brisanje bi moralo uspeti");
            Assert.AreEqual(initialCount, games.Count);
            Assert.IsNull(_gameService.GetGameById(newGame.Id));
        }

        // Test 9: Izbriši neobstoječo igro
        [TestMethod]
        public void Test_DeleteGame_ReturnsFalseForNonExistentId()
        {
            // Act
            var result = _gameService.DeleteGame(99999);

            // Assert
            Assert.IsFalse(result, "Brisanje neobstoječe igre mora vrniti false");
        }

        // Test 10: Preveri da privzete igre obstajajo
        [TestMethod]
        public void Test_GetAllGames_ContainsDefaultGames()
        {
            // Act
            var games = _gameService.GetAllGames();

            // Assert
            Assert.IsTrue(games.Any(g => g.Title.Contains("Zelda")), "Mora vsebovati Zelda igro");
            Assert.IsTrue(games.Any(g => g.Title.Contains("Elden Ring")), "Mora vsebovati Elden Ring");
            Assert.IsTrue(games.Any(g => g.Title.Contains("Minecraft")), "Mora vsebovati Minecraft");
        }

        // Test 11: Preveri filtriranje po oceni >= 9.0 (za ShowTopRated)
        [TestMethod]
        public void Test_GetAllGames_CanFilterTopRated()
        {
            // Arrange
            var allGames = _gameService.GetAllGames();

            // Act
            var topRatedGames = allGames.Where(g => g.Rating >= 9.0).ToList();

            // Assert
            Assert.IsTrue(topRatedGames.Count > 0, "Mora obstajati vsaj ena igra z oceno >= 9.0");
            Assert.IsTrue(topRatedGames.All(g => g.Rating >= 9.0), "Vse igre morajo imeti oceno >= 9.0");
        }

        // Test 12: Preveri da so vsi žanri različni (za dropdown)
        [TestMethod]
        public void Test_GetAllGames_HasMultipleGenres()
        {
            // Act
            var games = _gameService.GetAllGames();
            var distinctGenres = games.Select(g => g.Genre).Distinct().ToList();

            // Assert
            Assert.IsTrue(distinctGenres.Count >= 2, "Mora biti vsaj 2 različna žanra");
        }
    }
}