using Exiled.API.Features;
using System.Collections.Generic;
using System.Linq;

namespace SerpentsHand
{
    internal static class API
    {
        /// <summary>
        /// Checks if <see cref="Player"/> is Serpents Hand.
        /// </summary>
        /// <param name="player"> The player to check.</param>
        /// <returns><see langword="true"/> if player is Serpents Hand, <see langword="false"/> if not.</returns>
        internal static bool IsSerpent(Player player) => player.SessionVariables.ContainsKey("IsSH");

        /// <summary>
        /// Spawns <see cref="Player"/> as Serpents Hand.
        /// </summary>
        /// <param name="player"> The player to spawn.</param>
        /// <param name="full"> Should items and ammo be given to spawned <see cref="Player"/>.</param>
        internal static void SpawnPlayer(Player player, bool full = true) => Extensions.SpawnPlayer(player, full);

        /// <summary>
        /// Spawns Serpents Hand squad.
        /// </summary>
        /// <param name="playerList"> List of players to spawn.</param>
        internal static void SpawnSquad(List<Player> playerList) => Extensions.SpawnSquad(playerList);

        /// <summary>
        /// Spawns Serpents Hand squad.
        /// </summary>
        /// <param name="size"> The number of players in squad (this can be lower due to not enough number of Spectators).</param>
        internal static void SpawnSquad(uint size) => Extensions.SpawnSquad(size);

        /// <summary>
        /// Gets all alive Serpents Hand players.
        /// </summary>
        /// <returns><see cref="List{Player}"/> of all alive Serpents Hand players.</returns>
        internal static List<Player> GetSHPlayers() => Player.List.Where(x => x.SessionVariables.ContainsKey("IsSH")).ToList();
    }
}
