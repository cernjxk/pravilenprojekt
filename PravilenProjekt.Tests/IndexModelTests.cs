using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PravilenProjekt.Pages;
using PravilenProjekt.Services;
using System.Linq;

namespace PravilenProjekt.Tests
{
    [TestClass]
    public class IndexModelTests
    {
        private IndexModel _indexModel = null!;
        private GameService _gameService = null!;

        [TestInitialize]
        public void Setup()
        {
            _gameService = new GameService();
            _indexModel = new IndexModel(_gameService);
        }

        // Test 1: OnGet vrne vse igre brez filtrov
        [TestMethod]
        public void Test_OnGet_ReturnsAllGamesWithoutFilters()
        {
            // Act
            _indexModel.OnGet();

            // Assert
            Assert.IsNotNull(_indexModel.Games);
            Assert.IsTrue(_indexModel.Games.Count >= 3);
        }

        // Test 2: Iskanje po naslovu
        [TestMethod]
        public void Test_OnGet_SearchQueryFiltersGames()
        {
            // Arrange
            _indexModel.SearchQuery = "Zelda";

            // Act
            _indexModel.OnGet();

            // Assert
            Assert.IsTrue(_indexModel.Games.All(g => g.Title.Contains("Zelda", System.StringComparison.OrdinalIgnoreCase)));
        }

        // Test 3: Filter po žanru
        [TestMethod]
        public void Test_OnGet_GenreFilterFiltersGames()
        {
            // Arrange
            _indexModel.GenreFilter = "RPG";

            // Act
            _indexModel.OnGet();

            // Assert
            Assert.IsTrue(_indexModel.Games.All(g => g.Genre == "RPG"));
        }

        // Test 4: Filter po platformi
        [TestMethod]
        public void Test_OnGet_PlatformFilterFiltersGames()
        {
            // Arrange
            _indexModel.PlatformFilter = "Multi-platform";

            // Act
            _indexModel.OnGet();

            // Assert
            Assert.IsTrue(_indexModel.Games.All(g => g.Platform == "Multi-platform"));
        }

        // Test 5: ShowTopRated filtrira igre z oceno >= 9.0
        [TestMethod]
        public void Test_OnGet_ShowTopRatedFiltersGames()
        {
            // Arrange
            _indexModel.ShowTopRated = true;

            // Act
            _indexModel.OnGet();

            // Assert
            Assert.IsTrue(_indexModel.Games.All(g => g.Rating >= 9.0));
        }

        // Test 6: HasActiveFilters vrne false brez filtrov
        [TestMethod]
        public void Test_HasActiveFilters_ReturnsFalseWithoutFilters()
        {
            // Act
            var hasFilters = _indexModel.HasActiveFilters();

            // Assert
            Assert.IsFalse(hasFilters);
        }

        // Test 7: HasActiveFilters vrne true z iskanjem
        [TestMethod]
        public void Test_HasActiveFilters_ReturnsTrueWithSearchQuery()
        {
            // Arrange
            _indexModel.SearchQuery = "Test";

            // Act
            var hasFilters = _indexModel.HasActiveFilters();

            // Assert
            Assert.IsTrue(hasFilters);
        }

        // Test 8: GetTopRatedCount vrne pravilno število
        [TestMethod]
        public void Test_GetTopRatedCount_ReturnsCorrectCount()
        {
            // Act
            var count = _indexModel.GetTopRatedCount();
            var actualTopRated = _gameService.GetAllGames().Count(g => g.Rating >= 9.0);

            // Assert
            Assert.AreEqual(actualTopRated, count);
        }

        // Test 9: AvailableGenres so napolnjeni
        [TestMethod]
        public void Test_OnGet_PopulatesAvailableGenres()
        {
            // Act
            _indexModel.OnGet();

            // Assert
            Assert.IsNotNull(_indexModel.AvailableGenres);
            Assert.IsTrue(_indexModel.AvailableGenres.Count > 0);
        }

        // Test 10: AvailablePlatforms so napolnjeni
        [TestMethod]
        public void Test_OnGet_PopulatesAvailablePlatforms()
        {
            // Act
            _indexModel.OnGet();

            // Assert
            Assert.IsNotNull(_indexModel.AvailablePlatforms);
            Assert.IsTrue(_indexModel.AvailablePlatforms.Count > 0);
        }
    }
}