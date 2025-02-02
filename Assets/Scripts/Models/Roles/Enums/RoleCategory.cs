namespace Models.Roles.Enums
{
    public enum RoleCategory
    {
        FolkAnalyst = 0,
        FolkProtector = 1,
        FolkKilling = 2,
        FolkSupport = 3,
        FolkUnique = 4,

        CorruptorAnalyst = 5,
        CorruptorKilling = 6,
        CorruptorSupport = 7,

        NeutralEvil = 8,
        NeutralChaos = 9,
        NeutralKilling = 10,
        NeutralGood = 11
    }

    public static class RoleCategoryExtensions
    {
        public static int GetCategory(this RoleCategory roleCategory)
        {
            return (int)roleCategory;
        }
    }

}