using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.CorrupterRoles.Support
{
    public class Blinder: CorrupterRole, IActiveNightAbility
    {
        public Blinder() : base(RoleID.Blinder, RolePriority.Blinder, RoleCategory.CorruptorSupport, 0, 0){
        }
        
        public override bool ExecuteAbility() {
            string message = LanguageManager.GetText("Blinder","abilityMessage");
            SendAbilityMessage(message,roleOwner);
            List<Player> players = new List<Player>(GameScreenController.getGameService().getAlivePlayers());

            players.Remove(choosenPlayer);

            choosenPlayer.Role.SetChoosenPlayer(players[new Random().Next(players.Count())]);

            SendAbilityMessage(LanguageManager.GetText("Blinder","blindMessage"),choosenPlayer );

            return true;
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(25,10);
        }
    }
}