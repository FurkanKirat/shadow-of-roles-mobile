using Managers;
using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.NeutralRoles.Killing
{
    public class Assassin: NeutralRole, IActiveNightAbility
    {
        public Assassin(): base(RoleID.Assassin, RolePriority.None, RoleCategory.NeutralKilling, 1, 1) {
        }
        
        public override bool PerformAbility() {

            if(!IsCanPerform()){
                SendAbilityMessage(LanguageManager.GetText("RoleBlock","RBimmuneMessage") ,roleOwner);
            }

            if(choosenPlayer==null){
                return false;
            }

            if(choosenPlayer.IsImmune){
                SendAbilityMessage(LanguageManager.GetText("RoleBlock","immuneMessage"), roleOwner);
                return false;
            }
            return ExecuteAbility();

        }
        
        public override bool ExecuteAbility() {
            if(attack > choosenPlayer.Defence){
                choosenPlayer.SetAlive(false);
                choosenPlayer.SetCauseOfDeath(LanguageManager.GetText("CauseOfDeath","assassin"));
                SendAbilityMessage(LanguageManager.GetText("Assassin","killMessage"), roleOwner);
                SendAbilityAnnouncement(LanguageManager.GetText("Assassin","slainMessage")
                    .Replace("{playerName}",choosenPlayer.Name));
                return true;
            }
            
            SendAbilityMessage(LanguageManager.GetText("Assassin","defenceMessage"),
                roleOwner);
            return false;
            
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(40,1);
        }
        
        public override bool CanWinWithOtherTeams() {
            return false;
        }
    }
}