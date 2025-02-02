using Managers;
using Models.Roles.Enums;

namespace Models.Roles.FolkRoles
{
    public abstract class FolkRole : Role
    {
        public FolkRole(RoleID id, RolePriority rolePriority, RoleCategory roleCategory,
            double attack , double defence) : 
            base(id, rolePriority, roleCategory, Team.Folk, attack, defence) {
        }
        
        public override string GetGoal() {
            return LanguageManager.GetText("FolkRole","goal");
        }
    }
}