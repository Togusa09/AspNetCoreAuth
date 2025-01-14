namespace WebApp.Models
{
    public record Craft(string Name, bool SpaceWorthy)
    {
        public static Craft ThunderBird1 = new Craft("Thunderbird 1", true);
        public static Craft ThunderBird3 = new Craft("Thunderbird 3", true);
        public static Craft ThunderBird5 = new Craft("Thunderbird 5", true);
        public static Craft Mercury = new Craft("Mercury", true);
        public static Craft Apollo = new Craft("Apollo", true);

        public static Craft[] AllCraft = new Craft[]
        {
            ThunderBird1,
            ThunderBird3,
            ThunderBird5,
            Mercury,
            Apollo
        };
    }
}