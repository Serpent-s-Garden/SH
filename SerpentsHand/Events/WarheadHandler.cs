using Exiled.API.Enums;
using Exiled.API.Features;
using System.Linq;

namespace SerpentsHand.Events
{
    internal sealed class WarheadHandler
    {
        private Config config = SerpentsHand.Singleton.Config;

        internal void OnDetonated()
        {
            if (!config.SerpentsHandModifiers.WarheadKillsInPocket)
                return;

            foreach (Player player in Player.List.Where(x => x.CurrentRoom.Type == RoomType.Pocket && API.IsSerpent(x)))
                player.Kill("Vaporized by Alpha Warhead");
        }
    }
}
