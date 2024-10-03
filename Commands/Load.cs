using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.Permissions.Extensions;
using MEC;
using PlayerRoles;
using SCP_SL_SAVELOAD;

namespace SCP_SL_Test_Plugin.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Load : ICommand
    {
        public string Command { get; } = "load";

        public string[] Aliases { get; } = new[] { "ld","l" };

        public string Description { get; } = "Load your latest savestate.";
        public event EventHandler CanExecuteChanged;
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            Player player = Player.Get(sender);

            if (!SaveLoadPlugin.pluginInstance.Config.AllowedPlayers.Contains(player.UserId))
            {
                response = "You do not have permission to load a save.";
                return false;
            }

            if (!SaveLoadPlugin.SavePlayers.TryGetValue(player.UserId, out SaveState jail))
            {
                response = "No save state available for this player";
                return false;
            }
            player.ShowHint("Loading...",7);

            SaveState saveFile = SaveLoadPlugin.SavePlayers[player.UserId];
            player.Role.Set(saveFile.Role, RoleSpawnFlags.None);
            //newItems is meant to ultimately store every item the player has acquired since they last saved
            var newItems = new List<Item> { };
            foreach (Item item in player.Items)
            {
                newItems.Add(item);
            }
            var oldItems = new List<Item> { };
            var savedInv = new List<Item> { };
            foreach (Item item in player.Items)
            {
                savedInv.Add(item);
            }
            try
            {

                player.ClearInventory(false);
                foreach (Item savedItem in saveFile.Items)
                {
                    if (newItems.Contains(savedItem))
                    {
                        newItems.Remove(savedItem);
                    }
                    if (!player.HasItem(savedItem))
                    {
                        
                    }
                    player.RemoveItem(savedItem, false);

                    player.AddItem(savedItem.Clone()) ;

                }
                //drop new items (picked up since save) - works ok
                foreach (Item item in newItems)
                {
                    item.CreatePickup(player.Position, new UnityEngine.Quaternion(0, 0, 0, 0));
                    player.RemoveItem(item, false);
                }
                player.Health = saveFile.Health;
                player.ArtificialHealth = saveFile.AHP;
                player.Position = saveFile.RelativePosition.Position;
                foreach (KeyValuePair<AmmoType, ushort> kvp in saveFile.Ammo)
                    player.Ammo[kvp.Key.GetItemType()] = kvp.Value;
                player.SyncEffects(saveFile.Effects);

                player.Inventory.SendItemsNextFrame = true;
                player.Inventory.SendAmmoNextFrame = true;
            }
            catch (Exception e)
            {
                Log.Error($"Loading failed {player.DisplayNickname} : {e}");
            }

            //newItems here is to reset the saved inventory so they dont drop next time
            newItems.Clear();
            foreach (Item item in player.Items)
            {
                newItems.Add(item);
            }
            SaveLoadPlugin.SavePlayers[player.UserId].Items.Clear();
            SaveLoadPlugin.SavePlayers[player.UserId].Items = newItems;

            response = "Loaded!";
            // Return true if the command was executed successfully; otherwise, false.
            return true;
        }
    }
}
