using Managers;
using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.FolkRoles.Analyst
{
    public class Observer : FolkRole, IActiveNightAbility
    {
        public Observer()  : base(RoleID.Observer, RolePriority.None, RoleCategory.FolkAnalyst, 0,0){
        }
        
        public override bool ExecuteAbility() {
            SendAbilityMessage(LanguageManager.GetText("Observer","abilityMessage")
                    .Replace("{teamName}",GetChoosenPlayer().Role.GetTeam().ToString())
                ,GetRoleOwner());
            return true;
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(20,10);
        }
    }
}