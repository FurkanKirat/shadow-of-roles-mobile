using Managers;
using Models.Roles.Enums;

namespace Models.Roles.NeutralRoles
{
    public abstract class NeutralRole : Role
    {
        public NeutralRole(RoleID id, RolePriority rolePriority, RoleCategory roleCategory,
            double attack, double defence): base(id, rolePriority, roleCategory, Team.Neutral, attack, defence) {
        }
        
        public override string GetGoal() {
            return LanguageManager.GetText(id.ToString() ,"goal");
        }

        public abstract bool CanWinWithOtherTeams();
    }
}