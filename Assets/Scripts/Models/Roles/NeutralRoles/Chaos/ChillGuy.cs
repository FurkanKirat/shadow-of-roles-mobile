using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.NeutralRoles.Chaos
{
    public class ChillGuy: NeutralRole, INoNightAbility
    {
        public ChillGuy(): base(RoleID.ChillGuy, RolePriority.None, RoleCategory.NeutralChaos, 0, 0) {

        }
        
        public override bool PerformAbility() {
            return false;
        }
        
        public override bool ExecuteAbility() {
            return false;
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(10,1);
        }
        
        public override bool CanWinWithOtherTeams() {
            return true;
        }
    }
}