using AltV.Net;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.BuildingModels;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Repositories;
using LSG.DAL.UnitOfWork;
using LSG.GM.Entities.Core;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using VehicleDb = LSG.DAL.Database.Models.VehicleModels.Vehicle;

namespace LSG.GM.Economy.Offers
{
    public class Offer
    {
        public VehicleDb Vehicle { get; }
        public ItemModel Item { get; }
        public BuildingModel Building { get; }

        public int Money { get; }

        public object[] Args { get; }

        public CharacterEntity Sender { get; }
        public CharacterEntity Getter { get; }

        public Offer(CharacterEntity sender, CharacterEntity getter, ItemModel item, int money)
        {
            Item = item;
            Sender = sender;
            Getter = getter;
            Money = money;
        }

        public Offer(CharacterEntity sender, CharacterEntity getter, Action<CharacterEntity, CharacterEntity, object[]> action, object[] args, int money, bool moneyToGroup)
        {
            Sender = sender;
            Getter = getter;
            _action = action;
            Args = args;
            Money = money;
            _moneyToGroup = moneyToGroup;
        }

        private bool _moneyToGroup;
        private Action<CharacterEntity, CharacterEntity, object[]> _action;

        public void Trade(bool bankAccount)
        {
            if(Getter.HasEnoughMoney(Money, bankAccount))
            {
                if(_moneyToGroup && Sender.OnDutyGroup == null)
                {
                    Sender.AccountEntity.Player.SendChatMessageError("Musisz znajdować się na służbie grupy");
                    return;
                }

                if(Item != null)
                {
                    // TODO: Dorobić /me podaje przedmiot jakiś tam
                    // TODO: Sprawdzić czy usuwa przedmioty z ekwipunku

                    Item.Character = Getter.DbModel;


                    RoleplayContext ctx = Singleton.GetDatabaseInstance();
                    using (UnitOfWork unitOfWork = new UnitOfWork(ctx))
                    {
                        unitOfWork.ItemRepository.Update(Item);
                    }

                    Alt.Log($"[OFFER] Gracz {Sender.DbModel.Name} {Sender.DbModel.Surname} przekazał graczowi {Getter.DbModel.Name} {Getter.DbModel.Surname}  ITEM: {Item.Name} CENA: {Money}");
                }
                else if(Vehicle != null)
                {

                }


                if(_moneyToGroup)
                {
                    Sender.OnDutyGroup.AddMoney(Money);
                }
                else
                {
                    Sender.AddMoney(Money, bankAccount);
                }

                _action?.Invoke(Sender, Getter, Args);
                Getter.RemoveMoney(Money, bankAccount);

            }
            else
            {
                Getter.AccountEntity.Player.SendChatMessageError("Nie masz wystarczająco pieniędzy, aby wykonać tą transakcje");
                Sender.AccountEntity.Player.SendChatMessageError("Wymiana została przerwana, gracz nie ma środków na koncie");
            }
        }

        public void Dispose()
        {
            Getter.PendingOffer = false;
        }
    }
}
