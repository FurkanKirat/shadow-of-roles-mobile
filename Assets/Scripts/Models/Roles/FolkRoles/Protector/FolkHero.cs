using Managers;
using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.FolkRoles.Protector
{
    public class FolkHero : FolkRole, IActiveNightAbility
    {
        private int abilityUseCount;

        public FolkHero() : base(RoleID.FolkHero, RolePriority.FolkHero, RoleCategory.FolkProtector, 0, 0){
            abilityUseCount = 0;
        }
        
        public override bool PerformAbility() {

            if(GetChoosenPlayer()==null){
                return false;
            }

            abilityUseCount++;

            if(!IsCanPerform()){
                SendAbilityMessage(LanguageManager.GetText("RoleBlock","roleBlockedMessage") ,roleOwner);
                return false;
            }

            return ExecuteAbility();
        }
        
        public override bool ExecuteAbility() {
            if(abilityUseCount<=2){
                SendAbilityMessage(LanguageManager.GetText("RoleBlock","abilityMessage") ,roleOwner);
                choosenPlayer.SetImmune(true);
                return true;
            }
            return false;
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(5,1);
        }

        public int GetAbilityUseCount() {
            return abilityUseCount;
        }
    }
}