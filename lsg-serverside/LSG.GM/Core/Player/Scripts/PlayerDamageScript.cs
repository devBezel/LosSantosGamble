using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Core.Player.Scripts
{
    public class PlayerDamageScript : IScript
    {
        //public PlayerDamageScript()
        //{
        //    AltAsync.OnPlayerDamage += OnPlayerDamage;
        //}

        [AsyncScriptEvent(ScriptEventType.PlayerDamage)]
        public async Task OnPlayerDamage(IPlayer player, IEntity attacker, ushort oldHealth, ushort oldArmor, ushort oldMaxHealth, ushort oldMaxArmor, uint weapon, ushort damage) => await AltAsync.Do(() =>
        {
            if (player.Health >= 150) return;

            Alt.Log("wysyłam emit z damagem");

            player.EmitAsync("player-damage:drawFall");
        });
    }
}
