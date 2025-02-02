using System;
using enums;
using Models.Roles;

namespace Models
{
    public class Player
    {
        public int Number { get; }
        public string Name { get; }
        public Role Role { get; private set; }
        public bool IsAlive { get; private set; }
        public double Attack { get; private set; }
        public double Defence { get; private set; }
        public bool HasWon { get; private set; }
        public CauseOfDeath CauseOfDeath { get; private set; }
        public bool IsImmune { get; private set; }

        public Player(int number, string name, Role role)
        {
            Number = number;
            Name = name;
            Role = role;
            IsAlive = true;
            Attack = role.GetAttack();
            Defence = role.GetDefence();
            role.SetRoleOwner(this);
            HasWon = false;
            CauseOfDeath = CauseOfDeath.Alive;
        }

        public override string ToString()
        {
            return $"{Number}. {Name}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var player = (Player)obj;
            return Number == player.Number;
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }

        public void SetRole(Role role)
        {
            Role = role;
            Role.SetRoleOwner(this);
            Attack = role.GetAttack();
            Defence = role.GetDefence();
        }

        public void SetAlive(bool alive)
        {
            IsAlive = alive;
        }

        public void SetAttack(double attack)
        {
            Attack = attack;
        }

        public void SetDefence(double defence)
        {
            Defence = defence;
        }

        public void SetHasWon(bool hasWon)
        {
            HasWon = hasWon;
        }

        public void SetCauseOfDeath(CauseOfDeath causeOfDeath)
        {
            CauseOfDeath = causeOfDeath;
        }

        public void SetImmune(bool isImmune)
        {
            IsImmune = isImmune;
        }
    }
}
