using Managers;
using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.FolkRoles.Analyst
{
    public class Stalker : FolkRole, IActiveNightAbility
    {
        public Stalker() : base(RoleID.Stalker, RolePriority.None, RoleCategory.FolkAnalyst, 0, 0) {
        }

        
        public override bool ExecuteAbility() {
            string message = GetChoosenPlayer().Role.GetChoosenPlayer()==null ?
                LanguageManager.GetText("Stalker","nobodyMessage"):
                LanguageManager.GetText("Stalker","visitMessage")
                    .Replace("{playerName}", choosenPlayer.Role.GetChoosenPlayer().Name);
            SendAbilityMessage(message,roleOwner);
            return true;
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(25,10);
        }
    }
}