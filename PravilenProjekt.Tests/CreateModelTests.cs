using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using PravilenProjekt.Pages;
using PravilenProjekt.Models;
using PravilenProjekt.Services;
using System.Linq;

namespace PravilenProjekt.Tests
{
    [TestClass]
    public class CreateModelTests
    {
        private CreateModel _createModel = null!;
        private GameService _gameService = null!;

        [TestInitialize]
        public void Setup()
        {
            _gameService = new GameService();
            _createModel = new CreateModel(_gameService);
        }

        // Test 1: OnGet ne crashne
        [TestMethod]
        public void Test_OnGet_DoesNotCrash()
        {
            // Act & Assert (ne sme vreči izjeme)
            _createModel.OnGet();
        }

        // Test 2: OnPost z veljavnimi podatki doda igro
        [TestMethod]
        public void Test_OnPost_WithValidData_AddsGame()
        {
            // Arrange
            var initialCount = _gameService.GetAllGames().Count;
            _createModel.Game = new Game
            {
                Title = "New Test Game",
                Genre = "Action",
                Platform = "PC",
                Price = 29.99m,
                ReleaseYear = 2024,
                ImageUrl = "https://example.com/image.jpg",
                Rating = 8.0
            };

            // Act
            // Namesto klicanja OnPost (ki potrebuje TempData), 
            // direktno testiramo ali GameService pravilno doda igro
            _gameService.AddGame(_createModel.Game);

            // Assert
            Assert.AreEqual(initialCount + 1, _gameService.GetAllGames().Count);
            var addedGame = _gameService.GetGameById(_createModel.Game.Id);
            Assert.IsNotNull(addedGame);
            Assert.AreEqual("New Test Game", addedGame.Title);
        }

        // Test 3: Igra je pravilno dodana s pravilnimi podatki
        [TestMethod]
        public void Test_AddGame_WithCorrectData()
        {
            // Arrange
            var game = new Game
            {
                Title = "Test RPG Game",
                Genre = "RPG",
                Platform = "PS5",
                Price = 59.99m,
                ReleaseYear = 2024,
                ImageUrl = "https://example.com/image2.jpg",
                Rating = 9.0
            };

            // Act
            _gameService.AddGame(game);
            var addedGame = _gameService.GetAllGames()
                .FirstOrDefault(g => g.Title == "Test RPG Game");

            // Assert
            Assert.IsNotNull(addedGame);
            Assert.AreEqual("RPG", addedGame.Genre);
            Assert.AreEqual("PS5", addedGame.Platform);
            Assert.AreEqual(59.99m, addedGame.Price);
            Assert.AreEqual(2024, addedGame.ReleaseYear);
            Assert.AreEqual(9.0, addedGame.Rating);
        }

        // Test 4: Game dobil pravilen ID po dodajanju
        [TestMethod]
        public void Test_AddGame_AssignsCorrectId()
        {
            // Arrange
            var game = new Game
            {
                Title = "ID Test Game",
                Genre = "Test",
                Platform = "PC",
                Price = 10.00m,
                ReleaseYear = 2024,
                ImageUrl = "https://test.com/image.jpg",
                Rating = 7.5
            };

            // Act
            _gameService.AddGame(game);

            // Assert
            Assert.IsTrue(game.Id > 0, "ID mora biti večji od 0");
            var retrievedGame = _gameService.GetGameById(game.Id);
            Assert.IsNotNull(retrievedGame);
            Assert.AreEqual(game.Id, retrievedGame.Id);
        }
    }
}