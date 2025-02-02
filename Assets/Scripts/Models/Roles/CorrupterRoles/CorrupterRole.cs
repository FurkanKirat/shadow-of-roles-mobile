using Managers;
using Models.Roles.Enums;

namespace Models.Roles.CorrupterRoles
{
    public abstract class CorrupterRole : Role
    {
        public CorrupterRole(RoleID id, RolePriority rolePriority, RoleCategory roleCategory, double attack , double defence):
        base(id, rolePriority, roleCategory, Team.Corrupter, attack, defence){}
        
        public override string GetGoal() {
            return LanguageManager.GetText("CorrupterRole","goal");
        }
    }
}