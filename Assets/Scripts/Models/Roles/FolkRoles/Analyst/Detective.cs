using Managers;
using Models.Roles.Enums;
using Models.Roles.Interfaces;
using Unity.Mathematics;

namespace Models.Roles.FolkRoles.Analyst
{
    public class Detective : FolkRole, IActiveNightAbility

    {
    public Detective() :
        base(RoleID.Detective, RolePriority.None, RoleCategory.FolkAnalyst, 0, 0)
    {

    }

    public override bool ExecuteAbility()
    {

        Role randRole = RoleCatalog.GetRandomRole(GetChoosenPlayer().Role);

        var firstIsChosen = new Random().NextBool();
        var roleName1 = firstIsChosen ? GetChoosenPlayer().Role.GetName() : randRole.GetName();
        var roleName2 = firstIsChosen ? randRole.GetName() : GetChoosenPlayer().Role.GetName();

        string message = LanguageManager.GetText("Detective", "abilityMessage")
            .Replace("{roleName1}", roleName1)
            .Replace("{roleName2}", roleName2);

        SendAbilityMessage(message, GetRoleOwner());

        return true;
    }

    public override ChanceProperty GetChanceProperty()
    {
        return new ChanceProperty(30, 10);
    }

    }
}