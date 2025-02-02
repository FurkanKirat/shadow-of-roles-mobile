using System.Collections;
using System.Collections.Generic;
using Managers;
using Models.Roles.Enums;
using Models.Roles.Interfaces;
using Util;

namespace Models.Roles.CorrupterRoles.Analyst
{
    public class DarkSeer : CorrupterRole, IPassiveNightAbility
    {
        public DarkSeer() : base(RoleID.DarkSeer, RolePriority.None, RoleCategory.CorruptorAnalyst, 0, 0) {
        }
        
        public override bool PerformAbility() {
            if(!IsCanPerform()){
                SendAbilityMessage(LanguageManager.GetText("RoleBlock","roleBlockedMessage"),roleOwner);
                return false;
            }
            return ExecuteAbility();
        }
        
        public override bool ExecuteAbility() {
            List<Player> players = new List<Player>(GameScreenController.GameService.GetAlivePlayers());


            ListShuffler.Shuffle(players);
            string message;

            if (players.Count >= 2) {
                message = LanguageManager.GetText("Darkseer","abilityMessage")
                    .Replace("{roleName1}",players[0].Role.GetName())
                    .Replace("{roleName2}",players[1].Role.GetName());
            }
            else if (players.Count==1) {
                message = LanguageManager.GetText("Darkseer","oneLeftMessage")
                    .Replace("{roleName}",players[0].Role.GetName());
            }
            else{
                message = LanguageManager.GetText("Darkseer","zeroLeftMessage");
            }

            SendAbilityMessage(message,roleOwner);
            return true;
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(10,10);
        }
    }
}