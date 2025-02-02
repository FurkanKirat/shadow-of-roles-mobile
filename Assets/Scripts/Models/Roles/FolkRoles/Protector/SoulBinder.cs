using Managers;
using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Models.Roles.FolkRoles.Protector
{
    public class SoulBinder : FolkRole, IActiveNightAbility
    {
        public SoulBinder() : base(RoleID.SoulBinder, RolePriority.SoulBinder, RoleCategory.FolkProtector, 0,0){
        }
        
        public override bool ExecuteAbility() {
            SendAbilityMessage(LanguageManager.GetText("Soulbinder","abilityMessage") ,roleOwner);
            choosenPlayer.SetDefence(choosenPlayer.Defence+1);
            return true;
        }
        
        public override ChanceProperty GetChanceProperty() {
            return new ChanceProperty(20,10);
        }
    }
}