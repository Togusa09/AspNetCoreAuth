namespace WebApp.Models
{
    public record Craft(string Name, bool SpaceWorthy)
    {
        public static Craft Mercury = new Craft("Mercury", true);
        public static Craft Gemini = new Craft("Gemini", true);
        public static Craft Apollo = new Craft("Apollo", true);
        public static Craft Shuttle = new Craft("Shuttle", true);

        public static Craft[] AllCraft = new Craft[]
        {
            Mercury,
            Gemini,
            Apollo,
            Shuttle
        };
    }
}