using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.NeutralRoles.Chaos
{
    public class Clown : NeutralRole, INoNightAbility
    {
        public Clown(): base(RoleID.Clown, RolePriority.None, RoleCategory.NeutralChaos, 0, 0) {
        }
        
        public override bool PerformAbility() {
            return false;
        }
        
        public override bool ExecuteAbility() {
            return false;
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(30,1);
        }
        
        public override bool CanWinWithOtherTeams() {
            return true;
        }
    }
}