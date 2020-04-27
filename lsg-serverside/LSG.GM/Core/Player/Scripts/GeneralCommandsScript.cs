using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Entities.Core;
using LSG.GM.Enums;
using LSG.GM.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GM.Core.Player.Scripts
{
    public class GeneralCommandsScript : IScript
    {
        [Command("me", true)]
        public void MessageMeCMD(IPlayer player, string message)
        {
            player.SendChatMessageToNearbyPlayers(message, ChatType.Me);
        }

        [Command("do", true)]
        public void MessageDoCMD(IPlayer player, string message)
        {
            player.SendChatMessageToNearbyPlayers(message, ChatType.Do);
        }

        [Command("pm", false, new string[] { "pw", "w", "msg" })]
        public void MessagePMCMD(IPlayer sender, int getterId, params string[] message)
        {
            CharacterEntity characterEntitySender = sender.GetAccountEntity().characterEntity;
            
            IPlayer getter = PlayerExtenstion.GetPlayerById(getterId);

            if(getter == null)
            {
                sender.SendChatMessageError("Tego gracza nie ma w grze.");
                return;
            }

            CharacterEntity characterEntityGetter = getter.GetAccountEntity().characterEntity;
            sender.SendChatMessage("{c9ac53}(( > " + characterEntityGetter.DbModel.Name + " " + characterEntityGetter.DbModel.Surname + $"({getterId})" + $" {String.Join(" ", message)}" + " ))");
            getter.SendChatMessage("{e09c12}(( " + characterEntitySender.DbModel.Name + " " + characterEntitySender.DbModel.Surname + $"({characterEntitySender.AccountEntity.ServerID})" + $" {String.Join(" ", message)}" + " ))");
        }
    }
}
