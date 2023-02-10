using System;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using SerpentsHand.Commands.Subcmds;

namespace SerpentsHand.Commands
{
	[CommandHandler(typeof(RemoteAdminCommandHandler))]
	public class SerpentsHandCommand : ParentCommand
	{
		public SerpentsHandCommand() => LoadGeneratedCommands();

		public override string Command => "sh";
		public override string[] Aliases => Array.Empty<string>();
		public override string Description => "Parent command for Serpents Hand";

		public override void LoadGeneratedCommands()
		{
			RegisterCommand(List.Instance);
			RegisterCommand(Spawn.Instance);
			RegisterCommand(SpawnTeam.Instance);
		}

		protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
		{
			Player player = Player.Get(sender);
			response = "\nPlease enter a valid subcommand:\n";
			foreach (ICommand command in AllCommands)
				if (player.CheckPermission($"sh.{command.Command}"))
					response += $"- {command.Command} ({string.Join(", ", Aliases)})";
			return false;
		}
	}
}
