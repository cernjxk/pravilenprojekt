using Microsoft.VisualStudio.TestTools.UnitTesting;
using PravilenProjekt.Models;
using PravilenProjekt.Services;
using System.Collections.Generic;

namespace PravilenProjekt.Tests
{
    [TestClass]
    public class StatsServiceTests
    {
        private StatsService _svc = null!;

        [TestInitialize]
        public void Setup() => _svc = new StatsService();

        [TestMethod]
        public void Test_TotalGames_ReturnsCount()
        {
            var list = new List<PersonalGame>
            {
                new PersonalGame { Title = "A" },
                new PersonalGame { Title = "B" },
                new PersonalGame { Title = "C" }
            };

            Assert.AreEqual(3, _svc.TotalGames(list));
        }

        [TestMethod]
        public void Test_AverageRating_IgnoresNull()
        {
            var list = new List<PersonalGame>
            {
                new PersonalGame { Title = "A", Rating = 10 },
                new PersonalGame { Title = "B", Rating = null },
                new PersonalGame { Title = "C", Rating = 6 }
            };

            Assert.AreEqual(8.0, _svc.AverageRating(list), 0.0001);
        }

        [TestMethod]
        public void Test_MostCommonGenre_ReturnsTop()
        {
            var list = new List<PersonalGame>
            {
                new PersonalGame { Title = "A", Genre = "RPG" },
                new PersonalGame { Title = "B", Genre = "Action" },
                new PersonalGame { Title = "C", Genre = "rpg" }
            };

            Assert.AreEqual("RPG", _svc.MostCommonGenre(list));
        }
    }
}
