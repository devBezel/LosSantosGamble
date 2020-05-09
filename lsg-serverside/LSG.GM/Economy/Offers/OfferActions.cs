using LSG.GM.Entities.Core;
using LSG.GM.Extensions;
using LSG.GM.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Economy.Offers
{
    public static class OfferActions
    {

        public static void ResuscitationPlayerAction(CharacterEntity sender, CharacterEntity getter)
        {
            if (!getter.HasBw)
            {
                sender.AccountEntity.Player.SendChatMessageError("Ten gracz żyje, nie możesz go reanimować");
                return;
            }

            sender.AccountEntity.Player.PlayAnimation("mini@cpr@char_a@cpr_str", "cpr_pumpchest", 6000);

            // Do sprawdzenia
            Task.Run(async () =>
            {
                await Task.Delay(6000);

                getter.UnBw();
                getter.AccountEntity.Player.SendChatMessageInfo($"Zostałeś uleczony przez {sender.DbModel.Name} {sender.DbModel.Surname}.");
            });
        }
    }
}
