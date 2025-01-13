namespace WebApp.Models
{
    public record Craft(string Name)
    {
        public static Craft ThunderBird1 = new Craft("Thunderbird 1");
        public static Craft ThunderBird3 = new Craft("Thunderbird 3");
        public static Craft ThunderBird5 = new Craft("Thunderbird 5");
        public static Craft Mercury = new Craft("Mercury");
        public static Craft Apollo = new Craft("Apollo");
    }
}