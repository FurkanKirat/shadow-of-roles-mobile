using Managers;
using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.CorrupterRoles.Analyst
{
    public class DarkRevealer : CorrupterRole, IActiveNightAbility
    {
        public DarkRevealer() : base(RoleID.DarkRevealer, RolePriority.None, RoleCategory.CorruptorAnalyst, 0, 0){
        }
        
        public override bool ExecuteAbility() {

            string message = LanguageManager.GetText("DarkRevealer","abilityMessage")
                .Replace("{roleName}",choosenPlayer.Role.GetName());
            SendAbilityMessage(message,roleOwner);

            return true;
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(30,10);
        }
    }
}