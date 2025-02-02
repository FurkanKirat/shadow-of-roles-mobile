using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.CorrupterRoles.Support
{
    public class Disguiser: CorrupterRole, IActiveNightAbility
    {
        public Disguiser(): base(RoleID.Disguiser, RolePriority.None, RoleCategory.CorruptorSupport, 0, 0){
        }
        
        public override bool ExecuteAbility() {
            Role currentRole = RoleCatalog.GetRandomRole(new Disguiser());
            currentRole.SetChoosenPlayer(choosenPlayer);
            currentRole.SetRoleOwner(roleOwner);
            return true;
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(15,10);
        }
    }
}