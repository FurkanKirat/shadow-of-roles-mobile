using System;
using System.Collections.Generic;
using Models;

namespace Services
{
    public class VotingService
    {
        private Dictionary<Player, Player> votes = new Dictionary<Player, Player>();
        private Player maxVoted;
        private int maxVote;

        /// <summary>
        /// Casts a vote from the voter player to the voted player
        /// </summary>
        /// <param name="voter">voter player</param>
        /// <param name="voted">voted player</param>
        public void Vote(Player voter, Player voted)
        {
            votes[voter] = voted;
        }

        /// <summary>
        /// Returns the vote count of a player
        /// </summary>
        /// <param name="player">the desired player</param>
        /// <returns>player's vote count</returns>
        public int GetVoteCount(Player player)
        {
            int count = 0;
            foreach (var votedPlayer in votes.Values)
            {
                if (votedPlayer.Number == player.Number)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Updates the max voted player
        /// </summary>
        public void UpdateMaxVoted()
        {
            Dictionary<Player, int> voteCounts = new Dictionary<Player, int>();

            // Count votes for each player
            foreach (var votedPlayer in votes.Values)
            {
                if (votedPlayer != null)
                {
                    if (voteCounts.ContainsKey(votedPlayer))
                    {
                        voteCounts[votedPlayer]++;
                    }
                    else
                    {
                        voteCounts[votedPlayer] = 1;
                    }
                }
            }

            // Determine the player with the most votes
            foreach (var entry in voteCounts)
            {
                if (entry.Value > maxVote)
                {
                    maxVoted = entry.Key;
                    maxVote = entry.Value;
                }
            }
        }

        /// <summary>
        /// Clears the votes after the day is finished
        /// </summary>
        public void ClearVotes()
        {
            votes.Clear();
            maxVoted = null;
            maxVote = 0;
        }

        /// <summary>
        /// Nullifies all votes and resets voting data
        /// </summary>
        public void NullifyVotes()
        {
            votes.Clear();
            maxVoted = null;
            maxVote = 0;
        }

        // Getters
        public Player MaxVoted
        {
            get { return maxVoted; }
        }

        public int MaxVote
        {
            get { return maxVote; }
        }
    }
}
