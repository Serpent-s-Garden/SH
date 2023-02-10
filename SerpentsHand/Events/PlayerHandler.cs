using Exiled.API.Enums;
using Exiled.API.Features;
using PlayerStatsSystem;
using System.Collections.Generic;
using System.Linq;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;

namespace SerpentsHand.Events
{
    internal sealed class PlayerHandler
    {
        private Config config = SerpentsHand.Singleton.Config;

        internal void OnFailingEscapePocketDimension(FailingEscapePocketDimensionEventArgs ev)
        {
            if (API.IsSerpent(ev.Player) && !config.SerpentsHandModifiers.FriendlyFire)
            {
                ev.IsAllowed = false;
                if (config.SerpentsHandModifiers.TeleportTo106)
                    ev.Player.Position = Extensions.Get106Position();
            }
        }

        internal void OnEscapingPocketDimension(EscapingPocketDimensionEventArgs ev)
        {
            if (API.IsSerpent(ev.Player) && config.SerpentsHandModifiers.TeleportTo106)
                ev.TeleportPosition = Extensions.Get106Position();
        }

        internal void OnHurting(HurtingEventArgs ev)
        {
            List<Player> scp035s = Extensions.GetScp035s();

            if (ev.Attacker == null)
                return;

            if (((API.IsSerpent(ev.Player) && (ev.Attacker.Role.Team == Team.Dead || ev.DamageHandler.Type == DamageType.PocketDimension)) ||
                (API.IsSerpent(ev.Attacker) && (ev.Player.Role.Team == Team.Dead || (scp035s != null && scp035s.Contains(ev.Player)))) ||
                (API.IsSerpent(ev.Player) && API.IsSerpent(ev.Attacker) && ev.Player != ev.Attacker)) && !config.SerpentsHandModifiers.FriendlyFire)
                ev.IsAllowed = false;
        }

        internal void OnShooting(ShootingEventArgs ev)
        {
            Player target = Player.Get(ev.TargetNetId);
            if (target != null && target.Role == RoleTypeId.Scp096 && API.IsSerpent(ev.Player))
                ev.IsAllowed = false;
        }

        internal void OnActivatingGenerator(ActivatingGeneratorEventArgs ev)
        {
            if (API.IsSerpent(ev.Player) && !config.SerpentsHandModifiers.FriendlyFire)
                ev.IsAllowed = false;
        }

        internal void OnDestroying(DestroyingEventArgs ev)
        {
            if (API.IsSerpent(ev.Player))
                Extensions.DestroySH(ev.Player);
        }

        internal void OnDied(DiedEventArgs ev)
        {
            if (API.IsSerpent(ev.Player))
            {
                Extensions.DestroySH(ev.Player);
                return;
            }

            if (ev.Player.Role == RoleTypeId.Scp106 && !config.SerpentsHandModifiers.FriendlyFire)
                foreach (Player player in Player.List.Where(x => x.CurrentRoom.Type == RoomType.Pocket))
                    player.Hurt(new CustomReasonDamageHandler("WORLD", 50000f));
        }

        internal void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (API.IsSerpent(ev.Player) && ev.NewRole != RoleTypeId.Tutorial)
                Extensions.DestroySH(ev.Player);
        }

        internal void OnSpawningRagdoll(SpawningRagdollEventArgs ev)
        {
            if (!API.IsSerpent(ev.Player))
                return;

            ev.IsAllowed = false;
        }
    }
}
