namespace Models.Roles.Enums
{
    public enum RolePriority
    {
        None = 0,
        SoulBinder = 1,
        FolkHero = 2,
        Blinder = 3,
        RoleBlock = 4,
        LoreKeeper = 5,
        LastJoke = 6
        
    }
    public static class RolePriorityExtensions
    {
        public static int GetCategory(this RoleCategory roleCategory)
        {
            return (int)roleCategory;
        }
    }
}