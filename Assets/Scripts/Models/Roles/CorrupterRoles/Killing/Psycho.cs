using enums;
using Managers;
using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.CorrupterRoles.Killing
{
    public class Psycho : CorrupterRole, IActiveNightAbility
    {
        public Psycho() : base(RoleID.Psycho, RolePriority.None, RoleCategory.CorruptorKilling, 1,0) {
        }
        
        public override bool ExecuteAbility() {

            if(attack > choosenPlayer.Defence){
                this.choosenPlayer.SetAlive(false);
                this.choosenPlayer.SetCauseOfDeath(CauseOfDeath.Psycho);
                SendAbilityMessage(LanguageManager.GetText("Psycho","killMessage"), roleOwner);
                SendAbilityAnnouncement( LanguageManager.GetText("Psycho","slainMessage")
                    .Replace("{playerName}",this.choosenPlayer.Name));

                return true;
            }
            
            SendAbilityMessage(LanguageManager.GetText("Psycho","defenceMessage"), roleOwner);
            return false;
            
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(100,1);
        }
    }
}