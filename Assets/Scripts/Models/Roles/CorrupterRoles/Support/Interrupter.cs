using Managers;
using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.CorrupterRoles.Support
{
    public class Interrupter: CorrupterRole, IActiveNightAbility
    {
        public Interrupter(): base(RoleID.Interrupter, RolePriority.RoleBlock, RoleCategory.CorruptorSupport, 0, 0) {
        }
        
        public override bool PerformAbility() {

            if(choosenPlayer==null){
                return false;
            }

            if(!IsCanPerform()){
                SendAbilityMessage(LanguageManager.GetText("RoleBlock","RBimmuneMessage"), roleOwner);
            }

            if(choosenPlayer.IsImmune){
                SendAbilityMessage(LanguageManager.GetText("RoleBlock","immuneMessage"), roleOwner);
                return false;
            }

            return ExecuteAbility();

        }
        
        public override bool ExecuteAbility() {
            SendAbilityMessage(LanguageManager.GetText("RoleBlock","roleBlockMessage"), GetRoleOwner());
            choosenPlayer.Role.SetCanPerform(false);
            return true;
        }

        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(30,10);
        }
    }
}