using enums;
using Managers;
using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.CorrupterRoles.Support
{
    public class LastJoke : CorrupterRole, IActiveNightAbility
    {
        private bool didUsedAbility;
        public LastJoke(): base(RoleID.LastJoke, RolePriority.LastJoke, RoleCategory.CorruptorSupport, 3, 0) {
            didUsedAbility = false;
        }
        
        public override bool PerformAbility() {
            return ExecuteAbility();
        }
        
        public override bool ExecuteAbility() {
            if(!didUsedAbility && !roleOwner.IsAlive){
                didUsedAbility = true;

                if(choosenPlayer==null){
                    return false;
                }
                choosenPlayer.SetAlive(false);
                choosenPlayer.SetCauseOfDeath(CauseOfDeath.LastJoke);
                SendAbilityAnnouncement(LanguageManager.GetText("LastJoke","slainMessage")
                    .Replace("{playerName}", choosenPlayer.Name));

                return true;
            }

            return false;
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(5,1);
        }

        public bool isDidUsedAbility() {
            return didUsedAbility;
        }
    }
}