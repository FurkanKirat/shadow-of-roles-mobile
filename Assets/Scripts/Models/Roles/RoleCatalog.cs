using System;
using System.Collections.Generic;
using System.Linq;
using Models.Roles.CorrupterRoles.Analyst;
using Models.Roles.CorrupterRoles.Killing;
using Models.Roles.CorrupterRoles.Support;
using Models.Roles.Enums;
using Models.Roles.FolkRoles.Analyst;
using Models.Roles.FolkRoles.Protector;
using Models.Roles.FolkRoles.Support;
using Models.Roles.FolkRoles.Unique;
using Models.Roles.NeutralRoles.Chaos;
using Models.Roles.NeutralRoles.Good;
using Models.Roles.NeutralRoles.Killing;

namespace Models.Roles
{
    public class RoleCatalog
    {
        private static readonly Dictionary<Team, List<Role>> rolesMap = new Dictionary<Team, List<Role>>();
        private static readonly Dictionary<RoleCategory, List<Role>> categoryMap = new Dictionary<RoleCategory, List<Role>>();
        private static readonly List<Role> allRoles = new List<Role>();

        // Adds all roles to the catalog
        static RoleCatalog()
        {
            AddRole(new Detective(), new Psycho(), new Observer(), new SoulBinder(), new Stalker(),
                new DarkRevealer(), new Interrupter(), new SealMaster(), new Assassin(),
                new ChillGuy(), new Clown(), new Disguiser(), new DarkSeer(), new Blinder(),
                new LastJoke(), new FolkHero(), new Entrepreneur(), new LoreKeeper());
        }

        /// <summary>
        /// Adds role to the catalog
        /// </summary>
        private static void AddRole(params Role[] roles)
        {
            foreach (var role in roles)
            {
                if (!rolesMap.ContainsKey(role.GetTeam()))
                {
                    rolesMap[role.GetTeam()] = new List<Role>();
                }
                rolesMap[role.GetTeam()].Add(role);

                if (!categoryMap.ContainsKey(role.GetRoleCategory()))
                {
                    categoryMap[role.GetRoleCategory()] = new List<Role>();
                }
                categoryMap[role.GetRoleCategory()].Add(role);

                allRoles.Add(role);
            }
        }

        /// <summary>
        /// Gets roles by team
        /// </summary>
        public static List<Role> GetRolesByTeam(Team team)
        {
            return rolesMap.ContainsKey(team) ? rolesMap[team] : new List<Role>();
        }

        /// <summary>
        /// Gets roles by category
        /// </summary>
        public static List<Role> GetRolesByCategory(RoleCategory roleCategory)
        {
            return categoryMap.ContainsKey(roleCategory) ? categoryMap[roleCategory] : new List<Role>();
        }

        /// <summary>
        /// Gets all roles
        /// </summary>
        public static List<Role> GetAllRoles()
        {
            return new List<Role>(allRoles);
        }

        /// <summary>
        /// Gets a random role other than the specified one
        /// </summary>
        public static Role GetRandomRole(Role otherRole)
        {
            var otherRoles = new List<Role>(allRoles);
            otherRoles.Remove(otherRole);
            return otherRoles[new Random().Next(otherRoles.Count)].Copy();
        }

        /// <summary>
        /// Gets a random role
        /// </summary>
        public static Role GetRandomRole()
        {
            return allRoles[new Random().Next(allRoles.Count)].Copy();
        }

        /// <summary>
        /// Initializes roles based on player count
        /// </summary>
        public static List<Role> InitializeRoles(int playerCount)
        {
            Dictionary<Role, int> roles = playerCount switch
            {
                5 => ConfigureFivePlayers(),
                6 => ConfigureSixPlayers(),
                7 => ConfigureSevenPlayers(),
                8 => ConfigureEightPlayers(),
                9 => ConfigureNinePlayers(),
                10 => ConfigureTenPlayers(),
                _ => throw new InvalidOperationException($"Unexpected player count: {playerCount}")
            };

            var rolesList = new List<Role>();
            foreach (var entry in roles)
            {
                for (int i = 0; i < entry.Value; i++)
                {
                    rolesList.Add(entry.Key.Copy());
                }
            }

            rolesList = rolesList.OrderBy(r => new Random().Next()).ToList();
            return rolesList;
        }

        /// <summary>
        /// Configures roles for a 5-player game
        /// </summary>
        private static Dictionary<Role, int> ConfigureFivePlayers()
        {
            var roles = new Dictionary<Role, int>();
            PutRole(roles, GetRoleByCategoryWithProbability(roles, RoleCategory.FolkAnalyst));
            PutRole(roles, GetRoleByCategoryWithProbability(roles, RoleCategory.FolkSupport, RoleCategory.FolkProtector));
            PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Folk));
            PutRole(roles, GetRoleByCategoryWithProbability(roles, RoleCategory.CorruptorKilling));
            PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Neutral));

            return roles;
        }

        /// <summary>
        /// Configures roles for a 6-player game
        /// </summary>
        private static Dictionary<Role, int> ConfigureSixPlayers()
        {
            var roles = ConfigureFivePlayers();
            PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Folk));
            return roles;
        }

        /// <summary>
        /// Configures roles for a 7-player game
        /// </summary>
        private static Dictionary<Role, int> ConfigureSevenPlayers()
        {
            var roles = ConfigureSixPlayers();
            switch (new Random().Next(2))
            {
                case 0:
                    PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Neutral));
                    break;
                case 1:
                    PutRole(roles, GetRoleByCategoryWithProbability(roles, RoleCategory.CorruptorAnalyst, RoleCategory.CorruptorSupport));
                    break;
            }

            return roles;
        }

        /// <summary>
        /// Configures roles for an 8-player game
        /// </summary>
        private static Dictionary<Role, int> ConfigureEightPlayers()
        {
            var roles = ConfigureSixPlayers();
            switch (new Random().Next(3))
            {
                case 0:
                    PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Neutral));
                    PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Neutral));
                    break;
                case 1:
                    PutRole(roles, GetRoleByCategoryWithProbability(roles, RoleCategory.CorruptorAnalyst, RoleCategory.CorruptorSupport));
                    PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Neutral));
                    break;
                case 2:
                    PutRole(roles, GetRoleByCategoryWithProbability(roles, RoleCategory.CorruptorAnalyst, RoleCategory.CorruptorSupport));
                    PutRole(roles, GetRoleByCategoryWithProbability(roles, RoleCategory.CorruptorAnalyst, RoleCategory.CorruptorSupport));
                    break;
            }

            return roles;
        }

        /// <summary>
        /// Configures roles for a 9-player game
        /// </summary>
        private static Dictionary<Role, int> ConfigureNinePlayers()
        {
            var roles = ConfigureEightPlayers();
            PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Folk));
            return roles;
        }

        /// <summary>
        /// Configures roles for a 10-player game
        /// </summary>
        private static Dictionary<Role, int> ConfigureTenPlayers()
        {
            var roles = ConfigureSixPlayers();
            PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Folk));
            PutRole(roles, GetRoleByCategoryWithProbability(roles, RoleCategory.CorruptorSupport, RoleCategory.CorruptorAnalyst));

            switch (new Random().Next(4))
            {
                case 0:
                    PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Neutral));
                    PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Neutral));
                    break;
                case 1:
                    PutRole(roles, GetRoleByCategoryWithProbability(roles, RoleCategory.CorruptorAnalyst, RoleCategory.CorruptorSupport));
                    PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Neutral));
                    break;
                case 2:
                    PutRole(roles, GetRoleByCategoryWithProbability(roles, RoleCategory.CorruptorAnalyst, RoleCategory.CorruptorSupport));
                    PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Folk));
                    break;
                case 3:
                    PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Neutral));
                    PutRole(roles, GetRoleByTeamWithProbability(roles, Team.Folk));
                    break;
            }

            return roles;
        }

        private static void PutRole(Dictionary<Role, int> roles, Role role)
        {
            if (roles.ContainsKey(role))
            {
                roles[role]++;
            }
            else
            {
                roles[role] = 1;
            }
        }

        private static Role GetRoleByCategoryWithProbability(Dictionary<Role, int> currentRoles, RoleCategory roleCategory)
        {
            var roles = GetRolesByCategory(roleCategory).ToList();
            RemoveMaxCount(currentRoles, roles);
            return GetRoleWithProbability(roles);
        }

        private static Role GetRoleByCategoryWithProbability(Dictionary<Role, int> currentRoles, params RoleCategory[] roleCategories)
        {
            var roles = new List<Role>();
            foreach (var category in roleCategories)
            {
                roles.AddRange(GetRolesByCategory(category));
            }
            RemoveMaxCount(currentRoles, roles);
            return GetRoleWithProbability(roles);
        }

        private static Role GetRoleByTeamWithProbability(Dictionary<Role, int> currentRoles, Team team)
        {
            var roles = GetRolesByTeam(team).ToList();
            RemoveMaxCount(currentRoles, roles);
            return GetRoleWithProbability(roles);
        }

        private static void RemoveMaxCount(Dictionary<Role, int> currentRoles, List<Role> randomRoleList)
        {
            foreach (var entry in currentRoles)
            {
                if (entry.Key.GetChanceProperty().MaxNumber <= entry.Value)
                {
                    randomRoleList.Remove(entry.Key);
                }
            }
        }

        private static Role GetRoleWithProbability(List<Role> randomRoleList)
        {
            int sum = randomRoleList.Sum(role => role.GetChanceProperty().Chance);
            int randNum = new Random().Next(sum);
            int currentSum = 0;

            foreach (var role in randomRoleList)
            {
                currentSum += role.GetChanceProperty().Chance;

                if (currentSum >= randNum)
                {
                    return role.Copy();
                }
            }

            return null;
        }
    }
}
