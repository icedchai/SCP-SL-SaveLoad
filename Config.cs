using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Interfaces;


namespace SCP_SL_SAVELOAD 
{
    public class Config : IConfig
    {
        [Description("Indicates plugin enabled or not")]
        public bool IsEnabled { get; set; } = true;

        [Description("Indicates debug mode enabled or not")]
        public bool Debug { get; set; } = false;

        [Description("Steam/Discord IDs of players allowed to Save/Load")]
        public List<string> AllowedPlayers { get; set; } = new (){  };
    }
}
