using System.Collections.Generic;
using Managers;
using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.NeutralRoles.Good
{
    public class LoreKeeper: NeutralRole, IActiveNightAbility
    {
        private readonly List<Player> alreadyChosenPlayers;
        private Role guessedRole;
        private int trueGuessCount;
        public LoreKeeper(): 
            base(RoleID.LoreKeeper, RolePriority.LoreKeeper, RoleCategory.NeutralGood, 0, 0) {
            trueGuessCount = 0;
            alreadyChosenPlayers = new List<Player>();
        }
        
        public override bool PerformAbility() {
            if(choosenPlayer == null){
                return false;
            }

            if(guessedRole == null){
                return false;
            }
            return ExecuteAbility();
        }
        
        public override bool ExecuteAbility() {
            alreadyChosenPlayers.Add(choosenPlayer);

            if(choosenPlayer.Role.GetId() == guessedRole.GetId()){
                trueGuessCount++;

                string messageTemplate = LanguageManager.GetText("Lorekeeper","abilityMessage");

                string message = messageTemplate
                    .Replace("{playerName}", choosenPlayer.Name)
                    .Replace("{roleName}", choosenPlayer.Role.GetName());
                SendAbilityAnnouncement(message);
            }
            return false;
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(20,1);
        }

        public Role getGuessedRole() {
            return guessedRole;
        }

        public void setGuessedRole(Role guessedRole) {
            this.guessedRole = guessedRole;
        }

        public int getTrueGuessCount() {
            return trueGuessCount;
        }

        public void setTrueGuessCount(int trueGuessCount) {
            this.trueGuessCount = trueGuessCount;
        }

        public List<Player> getAlreadyChosenPlayers() {
            return alreadyChosenPlayers;
        }
        
        public override bool CanWinWithOtherTeams() {
            return true;
        }
    }
}