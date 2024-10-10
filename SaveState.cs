using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using PlayerRoles;
using RelativePositioning;
using System.Collections.Generic;

namespace SCP_SL_SAVELOAD
{
    public class SaveState
    {

        public string Name;
        public List<Item> Items;
        public List<Effect> Effects;
        public RoleTypeId Role;
        public RelativePosition RelativePosition;
        public float Health;
        public float AHP;
        public Dictionary<AmmoType, ushort> Ammo;
        public bool CurrentRound;

    }
}
