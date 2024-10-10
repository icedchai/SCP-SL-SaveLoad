using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;


namespace SCP_SL_SAVELOAD
{
    public class Config : IConfig
    {
        [Description("Indicates plugin enabled or not")]
        public bool IsEnabled { get; set; } = true;

        [Description("Indicates debug mode enabled or not")]
        public bool Debug { get; set; } = false;

        [Description("Steam/Discord IDs of players allowed to Save/Load")]
        public List<string> AllowedPlayers { get; set; } = new() { };
        [Description("% chance of the player encountering an out of date Node Graph")]
        public int NodeGraphChance { get; set; } = 20;
    }
}
