using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Core.Player.Scripts
{
    public class BwScript : IScript
    {
        private List<BwType> bwTypes = new List<BwType>();

        public BwScript()
        {
            Alt.OnPlayerDead += OnPlayerDeath;

            bwTypes.Add(new BwType() { Weapon = Weapons.AssaultRifle, Time = 5 });
            bwTypes.Add(new BwType() { Weapon = Weapons.RammedByCar, Time = 5 });
            bwTypes.Add(new BwType() { Weapon = Weapons.Wrench, Time = 5 });
            bwTypes.Add(new BwType() { Weapon = Weapons.RunOverByCar, Time = 5 });
            bwTypes.Add(new BwType() { Weapon = Weapons.Drowning, Time = 5 });
            bwTypes.Add(new BwType() { Weapon = Weapons.DrowningInVehicle, Time = 5 });

            Alt.OnClient("bw:gone", BwGone);
        }

        private void BwGone(IPlayer player,  object[] args)
        {
            if (player.GetAccountEntity() == null) return;
            player.GetAccountEntity().characterEntity.HasBw = false;

            player.Spawn(new Position(player.Position.X, player.Position.Y, player.Position.Z));
            player.Health = 200;
        }

        [Command("unbw")]
        public void UnBwKilledPlayer(IPlayer sender, int id)
        {
            if (sender.GetAccountEntity() == null) return;
            if (!sender.GetAccountEntity().HasRank((int)EAdmin.Administrator))
                return;

            if (!sender.GetAccountEntity().OnAdminDuty)
            {
                sender.SendErrorNotify("Wystąpił bląd!", "Aby użyć tej komendy musisz wejść na służbę administratora");
                return;
            }

            IPlayer getter = PlayerExtenstion.GetPlayerById(id);
            if (getter == null)
            {
                sender.SendErrorNotify(null, $"Gracz o ID {id} nie jest w grze");
                return;
            }
            if (!getter.GetAccountEntity().characterEntity.HasBw)
            {
                sender.SendErrorNotify(null, $"Ten gracz żyje, nie możesz nadać mu unbw");
                return;
            }

            getter.GetAccountEntity().characterEntity.Hunger = 100;
            getter.GetAccountEntity().characterEntity.Thirsty = 100;

            getter.Emit("bw:revive");
            getter.SendSuccessNotify(null, $"Otrzymałeś UNBW od administratora {sender.GetAccountEntity().DbModel.Username}");
        }

        private void OnPlayerDeath(IPlayer player, IEntity killer, uint weapon)
        {
            if (player.GetAccountEntity() == null) return;
            int time = 1;

            foreach (BwType type in bwTypes)
            {
                if(type.Weapon == weapon)
                {
                    time = type.Time;
                }
            }
      
            player.GetAccountEntity().characterEntity.HasBw = true;
            player.Emit("bw:timerStart", time);
        }
    }

    public class BwType
    {
        public uint Weapon { get; set; }
        public int Time { get; set; }
    }
}
