namespace PravilenProjekt.Services
{
    public class ThemeService
    {
        public string Normalize(string? theme)
        {
            if (string.IsNullOrWhiteSpace(theme)) return "light";
            theme = theme.Trim().ToLowerInvariant();
            return theme == "dark" ? "dark" : "light";
        }

        public string Toggle(string? currentTheme)
        {
            var cur = Normalize(currentTheme);
            return cur == "dark" ? "light" : "dark";
        }
    }
}
