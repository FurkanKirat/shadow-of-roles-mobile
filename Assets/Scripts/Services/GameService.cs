
using System.Collections.Generic;
using enums;
using Managers;
using Models;
using Models.Roles;
using Models.Roles.Enums;
using Models.Roles.Interfaces;

namespace Services
{
    public class GameService
    {
        private readonly List<Player> allPlayers = new List<Player>();
        private readonly List<Player> alivePlayers = new List<Player>();
        private readonly List<Player> deadPlayers = new List<Player>();
        private readonly VotingService votingService;
        private readonly TimeService timeService;
        private Player currentPlayer;
        private int currentPlayerIndex;
        private int playerCount;
        private Team winnerTeam;

        public GameService(List<string> names, List<Role> roles)
        {
            InitializePlayers(names, roles);
            timeService = new TimeService();
            votingService = new VotingService();
        }

        private void InitializePlayers(List<string> names, List<Role> roles)
        {
            playerCount = names.Count;

            for (int i = 0; i < playerCount; i++)
            {
                allPlayers.Add(new Player(i + 1, names[i], roles[i]));
            }

            UpdateAlivePlayers();
        }

        public void ToggleDayNightCycle()
        {
            timeService.ToggleTimeCycle();
            Time time = timeService.GetTime();
            switch (time)
            {
                case Time.DAY:
                    PerformAllAbilities();
                    break;
                case Time.NIGHT:
                    ExecuteMaxVoted();
                    break;
            }

            if (CheckGameFinished())
            {
                FinishGame();
            }
        }

        public void PerformAllAbilities()
        {
            List<Role> roles = new List<Role>(alivePlayers.ConvertAll(player => player.Role));
            roles.Sort((role1, role2) => role2.GetRolePriority().CompareTo(role1.GetRolePriority()));

            foreach (var role in roles)
            {
                role.PerformAbility();
            }

            foreach (var alivePlayer in alivePlayers)
            {
                alivePlayer.Role.SetChoosenPlayer(null);
                alivePlayer.SetDefence(alivePlayer.Role.GetDefence());
                alivePlayer.Role.SetCanPerform(true);
                alivePlayer.SetImmune(false);
            }

            UpdateAlivePlayers();
        }

        public void ExecuteMaxVoted()
        {
            votingService.UpdateMaxVoted();
            if (votingService.MaxVote > alivePlayers.Count / 2)
            {
                foreach (var alivePlayer in alivePlayers)
                {
                    if (alivePlayer.Number == votingService.MaxVoted.Number)
                    {
                        alivePlayer.IsAlive = false;
                        alivePlayer.CauseOfDeath = LanguageManager.GetText("CauseOfDeath", "hanging");
                        break;
                    }
                }

                UpdateAlivePlayers();

                if (votingService.MaxVoted != null)
                {
                    MessageService.SendMessage(LanguageManager.GetText("Message", "voteExecute")
                            .Replace("{playerName}", votingService.MaxVoted.Name)
                            .Replace("{roleName}", votingService.MaxVoted.Role.GetName()),
                        null, true, true);
                }
            }

            foreach (var player in alivePlayers)
            {
                player.Role.SetChoosenPlayer(null);
            }

            votingService.ClearVotes();
        }

        public void SendVoteMessages()
        {
            Player chosenPlayer = currentPlayer.Role.GetChoosenPlayer();
            if (timeService.GetTime() == Time.VOTING)
            {
                votingService.Vote(currentPlayer, chosenPlayer);

                if (chosenPlayer != null)
                {
                    MessageService.SendMessage(LanguageManager.GetText("Message", "vote")
                            .Replace("{playerName}", chosenPlayer.Name),
                        currentPlayer, false, true);
                }
                else
                {
                    MessageService.SendMessage(LanguageManager.GetText("Message", "noVote"), currentPlayer, false, true);
                }
            }
            else if (timeService.GetTime() == Time.NIGHT)
            {
                if (currentPlayer.Role is IActiveNightAbility)
                {
                    if (chosenPlayer != null)
                    {
                        MessageService.SendMessage(LanguageManager.GetText("Message", "ability")
                                .Replace("{playerName}", chosenPlayer.Name),
                            currentPlayer, false, false);
                    }
                    else
                    {
                        MessageService.SendMessage(LanguageManager.GetText("Message", "noAbilityUsed"), currentPlayer, false, false);
                    }
                }
            }
        }

        public void UpdateAlivePlayers()
        {
            alivePlayers.Clear();
            foreach (var player in allPlayers)
            {
                if (player.IsAlive)
                {
                    alivePlayers.Add(player);
                }
                else if (player.Role is LastJoke lastJoker && !lastJoker.IsDidUsedAbility && timeService.GetTime() == Time.NIGHT)
                {
                    alivePlayers.Add(player);
                }
            }

            if (alivePlayers.Count > 0)
            {
                currentPlayerIndex = 0;
                currentPlayer = alivePlayers[0];
            }
        }

        public List<Player> GetDeadPlayers()
        {
            deadPlayers.Clear();
            foreach (var player in allPlayers)
            {
                if (!player.IsAlive)
                {
                    deadPlayers.Add(player);
                }
            }
            return deadPlayers;
        }

        public bool CheckGameFinished()
        {
            if (alivePlayers.Count == 1)
            {
                winnerTeam = alivePlayers[0].Role.GetTeam();
                return true;
            }

            if (alivePlayers.Count == 0)
            {
                winnerTeam = Team.None;
                return true;
            }

            if (alivePlayers.Count == 2)
            {
                var player1 = alivePlayers[0];
                var player2 = alivePlayers[1];

                var neutralPlayer = alivePlayers.Find(p => p.Role is NeutralRole && ((NeutralRole)p.Role).CanWinWithOtherTeams);

                if (neutralPlayer != null)
                {
                    winnerTeam = player1.Role.GetTeam() == Team.Neutral ? player2.Role.GetTeam() : player1.Role.GetTeam();
                    return true;
                }

                if (player1.Role.GetTeam() != player2.Role.GetTeam() &&
                    player2.Role.GetAttack() <= player1.Role.GetDefence() &&
                    player1.Role.GetAttack() <= player2.Role.GetDefence())
                {
                    winnerTeam = Team.None;
                    return true;
                }
            }

            for (int i = 0; i < alivePlayers.Count - 1; i++)
            {
                if (alivePlayers[i].Role.GetTeam() != alivePlayers[i + 1].Role.GetTeam())
                {
                    return false;
                }
            }

            if (alivePlayers[0].Role.GetTeam() != Team.Neutral)
            {
                foreach (var alivePlayer in alivePlayers)
                {
                    winnerTeam = alivePlayer.Role.GetTeam();
                }
                return true;
            }

            return false;
        }

        public void FinishGame()
        {
            if (winnerTeam != Team.None && winnerTeam != Team.Neutral)
            {
                foreach (var player in allPlayers)
                {
                    if (player.Role.GetTeam() == winnerTeam)
                    {
                        player.SetHasWon(true);
                    }
                }
            }

            if (winnerTeam == Team.Neutral)
            {
                alivePlayers[0].SetHasWon(true);
            }

            bool chillGuyExist = false;
            foreach (var player in allPlayers)
            {
                switch (player.Role)
                {
                    case ChillGuy _:
                        chillGuyExist = true;
                        break;
                    case Clown _ when !player.IsAlive && player.CauseOfDeath != LanguageManager.GetText("CauseOfDeath", "hanging"):
                        player.HasWon = true;
                        break;
                    case Lorekeeper lorekeeper:
                        int winCount = playerCount > 6 ? 3 : 2;
                        if (lorekeeper.TrueGuessCount >= winCount)
                        {
                            player.HasWon = true;
                        }
                        break;
                }
            }

            if (chillGuyExist)
            {
                SceneManager.SwitchScene("/com/rolegame/game/fxml/game/ChillGuyAlert.fxml", SceneManager.SceneType.SIMPLE_PERSON_ALERT, false);
            }
            else
            {
                SceneManager.SwitchScene("/com/rolegame/game/fxml/game/EndGame.fxml", SceneManager.SceneType.END_GAME, false);
            }

            MessageService.ResetMessages();
            votingService.NullifyVotes();
        }

        public void PassTurn()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % alivePlayers.Count;
            currentPlayer = alivePlayers[currentPlayerIndex];

            if (currentPlayerIndex == 0)
            {
                ToggleDayNightCycle();
            }
        }

        // Getters and Setters
        public int CurrentPlayerIndex
        {
            get => currentPlayerIndex;
            set => currentPlayerIndex = value;
        }

        public Player CurrentPlayer
        {
            get => currentPlayer;
            set => currentPlayer = value;
        }

        public Team WinnerTeam
        {
            get => winnerTeam;
            set => winnerTeam = value;
        }

        public List<Player> AllPlayers => allPlayers;
        public List<Player> AlivePlayers => alivePlayers;
        public TimeService TimeService => timeService;
    }
}
