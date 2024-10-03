using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.CustomRoles.Events;
using Exiled.Events.Handlers;
using MEC;
using PluginAPI.Events;
using SCP_SL_SAVELOAD.EventHandlers;
using Player = Exiled.Events.Handlers.Player;

namespace SCP_SL_SAVELOAD
{
    public class SaveLoadPlugin :  Plugin<Config>
    {

        public override string Name => "SCP Save/Load";

        public override string Author => "icedchqi";

        public override string Prefix => "omni-gordonfreeman-agentissac";
        public static SaveLoadPlugin pluginInstance { get; set; }
        public override Version Version => new(1, 0, 0);
        PluginEventHandler EventHandler;
        public static Dictionary<string, SaveState> SavePlayers { get; } = new();
        public static Dictionary<string,int> LoadCooldown { get; } = new();
        public static Dictionary<string, int> SaveCooldown{ get; } = new();
        public override void OnEnabled()
        {
            pluginInstance=this;
            RegisterEvents();
            
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
        }
        private void RegisterEvents()
        {

            EventHandler = new PluginEventHandler();
            
        }

        private void UnregisterEvents()
        {

            EventHandler = null;
        }
    }
}
