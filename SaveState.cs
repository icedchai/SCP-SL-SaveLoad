using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Roles;
using Exiled.API.Interfaces;
using PlayerRoles;
using RelativePositioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
