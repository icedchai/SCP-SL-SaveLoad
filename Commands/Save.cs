using CommandSystem;
using Exiled.API.Extensions;
using Exiled.API.Features;
using System;
using System.Linq;

namespace SCP_SL_SAVELOAD.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Save : ICommand
    {
        public string Command { get; } = "save";

        public string[] Aliases { get; } = new[] { "s" };

        public string Description { get; } = "Create a character savestate that you may later load.";
        public event EventHandler CanExecuteChanged;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (!SaveLoadPlugin.pluginInstance.Config.AllowedPlayers.Contains(player.UserId))
            {
                response = "You do not have permission to create a savestate.";
                return false;
            }
            if (player.IsDead)
            {
                response = "You cannot save a state while dead";
                return false;
            }
            if (SaveLoadPlugin.SavePlayers.ContainsKey(player.UserId))
            {
                SaveLoadPlugin.SavePlayers.Remove(player.UserId);
                SaveLoadPlugin.SavePlayers.Add(player.UserId, new SaveState
                {
                    Health = player.Health,
                    AHP = player.ArtificialHealth,
                    RelativePosition = player.RelativePosition,
                    Items = player.Items.ToList(),
                    Effects = player.ActiveEffects.Select(x => new Effect(x)).ToList(),
                    Name = player.Nickname,
                    Role = player.Role.Type,
                    CurrentRound = true,
                    Ammo = player.Ammo.ToDictionary(x => x.Key.GetAmmoType(), x => x.Value),
                });
            }
            else
            {
                SaveLoadPlugin.SavePlayers.Add(player.UserId, new SaveState
                {
                    Health = player.Health,
                    RelativePosition = player.RelativePosition,
                    Items = player.Items.ToList(),
                    Effects = player.ActiveEffects.Select(x => new Effect(x)).ToList(),
                    Name = player.Nickname,
                    Role = player.Role.Type,
                    CurrentRound = true,
                    Ammo = player.Ammo.ToDictionary(x => x.Key.GetAmmoType(), x => x.Value),
                });
            }
            player.ShowHint("Saved.");
            response = "Saved!";
            // Return true if the command was executed successfully; otherwise, false.
            return true;
        }
    }
}
