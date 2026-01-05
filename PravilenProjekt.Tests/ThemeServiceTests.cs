using Microsoft.VisualStudio.TestTools.UnitTesting;
using PravilenProjekt.Services;

namespace PravilenProjekt.Tests
{
    [TestClass]
    public class ThemeServiceTests
    {
        private ThemeService _svc = null!;

        [TestInitialize]
        public void Setup() => _svc = new ThemeService();

        [TestMethod]
        public void Test_Normalize_DefaultsToLight()
        {
            Assert.AreEqual("light", _svc.Normalize(null));
            Assert.AreEqual("light", _svc.Normalize(""));
            Assert.AreEqual("light", _svc.Normalize("  "));
        }

        [TestMethod]
        public void Test_Normalize_DarkAccepted()
        {
            Assert.AreEqual("dark", _svc.Normalize("dark"));
            Assert.AreEqual("dark", _svc.Normalize(" DARK "));
        }

        [TestMethod]
        public void Test_Toggle_SwitchesTheme()
        {
            Assert.AreEqual("dark", _svc.Toggle("light"));
            Assert.AreEqual("light", _svc.Toggle("dark"));
            Assert.AreEqual("dark", _svc.Toggle(null)); // null -> light -> dark
        }
    }
}
