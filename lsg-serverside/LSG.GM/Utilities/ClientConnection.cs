using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Utilities
{
    public class ClientConnection : IScript
    {
        public static Dictionary<ulong, Dictionary<string, Action<IPlayer, object[]>>> PlayerCallback = new Dictionary<ulong, Dictionary<string, Action<IPlayer, object[]>>>();


        public async Task OnPlayerEvent(IPlayer player, string eventName, object[] args) => await AltAsync.Do(async () =>
        {
            Alt.Log("Wykonuje OnPlayerEvent");
            string playerName = await player.GetNameAsync();
            if(PlayerCallback.ContainsKey(player.SocialClubId))
            {
                Dictionary<string, Action<IPlayer, object[]>> currentPlayerCallback = PlayerCallback[player.SocialClubId];
                if(currentPlayerCallback.ContainsKey(eventName))
                {
                    currentPlayerCallback[eventName](player, args);
                    currentPlayerCallback.Remove(eventName);
                }
            }
        });

        public static void CallbackClient(IPlayer player, string EventName, Action<IPlayer, object[]> MySpecifitedAction, object[] argToSend)
        {
            string CallbackName = EventName + "_return";

            if(PlayerCallback.ContainsKey(player.SocialClubId))
            {
                Dictionary<string, Action<IPlayer, object[]>> currentPlayerCallback = PlayerCallback[player.SocialClubId];

                if(currentPlayerCallback.ContainsKey(CallbackName))
                {
                    currentPlayerCallback.Remove(CallbackName);
                }

                currentPlayerCallback.Add(CallbackName, MySpecifitedAction);
            }
            else
            {
                Dictionary<string, Action<IPlayer, object[]>> DefaultDictionary = new Dictionary<string, Action<IPlayer, object[]>>();
                DefaultDictionary.Add(CallbackName, MySpecifitedAction);
                PlayerCallback.Add(player.SocialClubId, DefaultDictionary);
            }

            player.Emit(EventName, argToSend);
        }
    }
}
