using System;
using Managers;
using Models.Roles.CorrupterRoles.Analyst;
using Models.Roles.CorrupterRoles.Killing;
using Models.Roles.Enums;
using Models.Roles.FolkRoles.Analyst;
using Models.Roles.FolkRoles.Protector;
using Models.Roles.Interfaces;

namespace Models.Roles.FolkRoles.Unique
{
    public class Entrepreneur : FolkRole, IActiveNightAbility
    {
        
    private const int HEAL_PRICE = 3;
    private const int INFO_PRICE = 2;
    private const int ATTACK_PRICE = 4;
    private int _money;
    private ChosenAbility abilityState;
    public Entrepreneur() : base (RoleID.Entrepreneur, RolePriority.None, RoleCategory.FolkUnique, 0, 0){
        _money = 3;
        SetAbilityState(ChosenAbility.NONE);
    }
    
    public override bool ExecuteAbility()
    {
        rolePriority = RolePriority.None;

        switch (abilityState)
        {
            case ChosenAbility.ATTACK:
                if (_money >= ATTACK_PRICE)
                {
                    _money -= ATTACK_PRICE;
                    return UseOtherAbility(new Psycho());
                }
                break;

            case ChosenAbility.HEAL:
                if (_money >= HEAL_PRICE)
                {
                    _money -= HEAL_PRICE;
                    return UseOtherAbility(new SoulBinder());
                }
                break;

            case ChosenAbility.INFO:
                if (_money >= INFO_PRICE)
                {
                    _money -= INFO_PRICE;
                    return GatherInfo();
                }
                break;

            default:
                return false;
        }

        return InsufficientMoney();
    }

    
    public override ChanceProperty GetChanceProperty() {
        return new ChanceProperty(15,1);
    }

    public ChosenAbility GetAbilityState() {
        return abilityState;
    }

    public void SetAbilityState(ChosenAbility abilityState) {
        this.abilityState = abilityState;
    }

    private bool GatherInfo()
    {
        Role role;
        switch (new Random().Next(5))
        {
            case 0:
                role = new DarkSeer();
                break;
            case 1:
                role = new Detective();
                break;
            case 2:
                role = new Observer();
                break;
            case 3:
                role = new Stalker();
                break;
            default:
                role = new DarkRevealer();
                break;
        }

        return UseOtherAbility(role);
    }


    private bool UseOtherAbility(Role role){
        role.SetChoosenPlayer(GetChoosenPlayer());
        role.SetRoleOwner(GetRoleOwner());
        return role.ExecuteAbility();
    }

    private bool InsufficientMoney(){
        string message = LanguageManager.GetText("Entrepreneur","insufficientMoney");

        switch (abilityState){
            case ChosenAbility.ATTACK: message += LanguageManager.GetText("Entrepreneur","attack");
                break;
            case ChosenAbility.HEAL : message += LanguageManager.GetText("Entrepreneur", "heal");
                break;
            case ChosenAbility.INFO : message += LanguageManager.GetText("Entrepreneur","info");
                break;
        }
        SendAbilityMessage(message, roleOwner);
        return false;
    }


    public enum ChosenAbility{
        ATTACK,
        HEAL,
        INFO,
        NONE
    }

    public int GetMoney() {
        return _money;
    }

    public void SetMoney(int money) {
        this._money = money;
    }
    }
}