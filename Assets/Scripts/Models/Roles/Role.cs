using System;
using Managers;
using Models.Roles.Enums;
using Services;

namespace Models.Roles
{
    public abstract class Role
    {
        protected RoleID id;
        protected RolePriority rolePriority;
        protected RoleCategory roleCategory;
        protected Team team;
        protected Player roleOwner;
        protected Player choosenPlayer;
        protected double attack;
        protected double defence;
        protected bool canPerform;

        public Role(RoleID id, RolePriority rolePriority, RoleCategory roleCategory,
                    Team team, double attack, double defence)
        {
            // IMPORTANT! When adding a new role, the role id and role name in the lang json files must be the same!
            this.id = id;
            this.rolePriority = rolePriority;
            this.roleCategory = roleCategory;
            this.team = team;
            this.attack = attack;
            this.defence = defence;
            this.canPerform = true;
        }

        public Role(Role role)
        {
            this.id = role.id;
            this.rolePriority = role.rolePriority;
            this.roleCategory = role.roleCategory;
            this.team = role.team;
            this.attack = role.GetAttack();
            this.defence = role.GetDefence();
            this.canPerform = true;

            this.roleOwner = role.roleOwner;
            this.choosenPlayer = roleOwner.Role.GetChoosenPlayer();
        }

        public Role Copy()
        {
            try
            {
                return (Role)Activator.CreateInstance(this.GetType());
            }
            catch (Exception e)
            {
                throw new Exception("Cannot create copy of Role", e);
            }
        }

        public virtual bool PerformAbility()
        {
            if (!canPerform)
            {
                SendAbilityMessage(LanguageManager.GetText("RoleBlock", "roleBlockedMessage"), roleOwner);
                return false;
            }

            if (choosenPlayer == null)
            {
                return false;
            }

            if (choosenPlayer.IsImmune)
            {
                SendAbilityMessage(LanguageManager.GetText("RoleBlock", "immuneMessage"), roleOwner);
                return false;
            }

            return ExecuteAbility();
        }

        protected void SendAbilityMessage(string message, Player receiver)
        {
            MessageService.SendMessage(message, receiver, false, false);
        }

        protected void SendAbilityAnnouncement(string message)
        {
            MessageService.SendMessage(message, null, true, false);
        }

        public abstract bool ExecuteAbility();

        public override string ToString()
        {
            return GetName();
        }

        // Getter and Setters

        public RoleID GetId() => id;

        public string GetName()
        {
            return LanguageManager.GetRoleText(id.ToString(), "name");
        }

        public string GetAttributes()
        {
            return LanguageManager.GetText(id.ToString(), "attributes");
        }

        public string GetAbilities()
        {
            return LanguageManager.GetText(id.ToString(), "abilities");
        }

        public abstract string GetGoal();

        public RolePriority GetRolePriority() => rolePriority;

        public Team GetTeam() => team;

        public Player GetChoosenPlayer() => choosenPlayer;

        public void SetChoosenPlayer(Player choosenPlayer)
        {
            this.choosenPlayer = choosenPlayer;
        }

        public double GetAttack() => attack;

        public void SetAttack(double attack)
        {
            this.attack = attack;
        }

        public double GetDefence() => defence;

        public void SetDefence(double defence)
        {
            this.defence = defence;
        }

        public Player GetRoleOwner() => roleOwner;

        public void SetRoleOwner(Player roleOwner)
        {
            this.roleOwner = roleOwner;
        }

        public bool IsCanPerform() => canPerform;

        public void SetCanPerform(bool canPerform)
        {
            this.canPerform = canPerform;
        }

        public RoleCategory GetRoleCategory() => roleCategory;

        public void SetRolePriority(RolePriority rolePriority)
        {
            this.rolePriority = rolePriority;
        }

        public abstract ChanceProperty GetChanceProperty();

        public record ChanceProperty
        {
            public int Chance { get; init; }
            public int MaxNumber { get; init; }

            // Constructor
            public ChanceProperty(int chance, int maxNumber)
            {
                Chance = chance;
                MaxNumber = maxNumber;
            }
        }
    }
}
