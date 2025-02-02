using Managers;
using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.FolkRoles.Support
{
    public class SealMaster : FolkRole, IActiveNightAbility
    {
        public SealMaster() : base(RoleID.SealMaster, RolePriority.RoleBlock, RoleCategory.FolkSupport, 0,0){
        }
        
        public override bool PerformAbility() {

            if(GetChoosenPlayer()==null){
                return false;
            }

            if(!IsCanPerform()){
                SendAbilityMessage(LanguageManager.GetText("RoleBlock","RBimmuneMessage") ,roleOwner);
            }

            if(choosenPlayer.IsImmune){
                SendAbilityMessage(LanguageManager.GetText("RoleBlock","immuneMessage") ,roleOwner);
                return false;
            }
            return ExecuteAbility();
        }
        
        public override bool ExecuteAbility() {

            SendAbilityMessage(LanguageManager.GetText("RoleBlock","roleBlockMessage"), roleOwner);
            choosenPlayer.Role.SetCanPerform(false);
            return true;
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(30,10);
        }
    }
}