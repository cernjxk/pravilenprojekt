using Microsoft.VisualStudio.TestTools.UnitTesting;
using PravilenProjekt.Services;

namespace PravilenProjekt.Tests
{
    [TestClass]
    public class PersonalLibraryValidatorTests
    {
        private PersonalLibraryService _svc = null!;

        [TestInitialize]
        public void Setup() => _svc = new PersonalLibraryService();

        [TestMethod]
        public void Test_IsValidTitle_RejectsEmpty()
        {
            Assert.IsFalse(_svc.IsValidTitle(null));
            Assert.IsFalse(_svc.IsValidTitle(""));
            Assert.IsFalse(_svc.IsValidTitle("   "));
        }

        [TestMethod]
        public void Test_IsValidRating_AllowsNullAndRange()
        {
            Assert.IsTrue(_svc.IsValidRating(null));
            Assert.IsTrue(_svc.IsValidRating(1));
            Assert.IsConfirm(_svc.IsValidRating(10));

            Assert.IsFalse(_svc.IsValidRating(0));
            Assert.IsFalse(_svc.IsValidRating(11));
        }

        [TestMethod]
        public void Test_CreateGame_AssignsIdAndTrims()
        {
            var g = _svc.CreateGame("  My Game  ", "  Action  ", 8);
            Assert.IsTrue(g.Id > 0);
            Assert.AreEqual("My Game", g.Title);
            Assert.AreEqual("Action", g.Genre);
            Assert.AreEqual(8, g.Rating);
        }
    }

    // mali helper, da imaš čist Assert (če hočeš)
    internal static class Assert
    {
        public static void IsConfirm(bool condition)
        {
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(condition);
        }
    }
}
