using AltV.Net;
using AltV.Net.Async;
using LSG.DAL.Database;
using LSG.DAL.Database.Models.ItemModels;
using LSG.DAL.Database.Models.SmartphoneModels;
using LSG.DAL.Enums;
using LSG.DAL.UnitOfWork;
using LSG.GM.Entities.Common.SmartphoneOpt;
using LSG.GM.Extensions;
using LSG.GM.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSG.GM.Entities.Core.Item
{
    public class Smartphone : ItemEntity
    {
        public int PhoneId => DbModel.Id;
        public SmartphoneType SmartphoneType => (SmartphoneType)DbModel.FirstParameter;
        public int SmartphoneNumber => (int)DbModel.SecondParameter;
        public int SmartphoneCredit => (int)DbModel.ThirdParameter;
        public int SmartphoneMemory => (int)DbModel.FourthParameter;

        public List<SmartphoneContactModel> SmartphoneContacts => DbModel.SmartphoneContacts;
        public List<SmartphoneRecentCallModel> SmartphoneRecentCalls => DbModel.SmartphoneRecentCalls;
        //public List<SmartphoneMessageModel> SmartphoneMessages => DbModel.SmartphoneMessages;
        public List<SmartphoneMessageModel> SmartphoneMessages { get; set; }

        public bool IsTalking { get; set; }

        public SmartphoneIncomingCallModel IncomingCall { get; set; }
        public SmartphoneCall CurrentCall { get; set; }

        public Smartphone(ItemModel item) : base(item)
        {
            SmartphoneMessages = new List<SmartphoneMessageModel>();

            SmartphoneMessages = Singleton.GetDatabaseInstance().SmartphoneMessages.Include(c => c.Cellphone).Where(x => x.GetterNumber == SmartphoneNumber).ToList();

            foreach (SmartphoneMessageModel message in DbModel.SmartphoneMessages)
            {
                if (message != null)
                {
                    if (!SmartphoneMessages.Any(x => x.Id == message.Id))
                    {
                        SmartphoneMessages.Add(message);
                    }

                }
            }
        }

        public override void UseItem(CharacterEntity sender)
        {

            if(sender.ItemsInUse.Any(item => item is Smartphone && !ReferenceEquals(item, this)))
            {
                sender.AccountEntity.Player.SendChatMessageError("Możesz mieć wlączony tylko jeden telefon!");
                return;
            }

            if(sender.ItemsInUse.Any(item => ReferenceEquals(item, this)))
            {
                DbModel.ItemInUse = false;

                sender.ItemsInUse.Remove(this);
                sender.CurrentSmartphone = null;

                EntityHelper.Remove(this);
            }
            else
            {

                DbModel.ItemInUse = true;

                sender.ItemsInUse.Add(this);
                sender.CurrentSmartphone = this;

                EntityHelper.Add(this);
                // TODO: wysylanie wszystkich danych
            }
        }


        public void SendMessage(CharacterEntity sender, int smartphoneId, int getterNumber, string message)
        {
            SmartphoneMessageModel smartphoneMessage = new SmartphoneMessageModel()
            {
                GetterNumber = getterNumber,
                Cellphone = sender.CurrentSmartphone.DbModel,
                CellphoneId = smartphoneId,
                Message = message,
                IsRead = false,
                Date = DateTime.Now
            };

            SmartphoneMessages.Add(smartphoneMessage);

            CharacterEntity getter = EntityHelper.GetCharacterBySmartphoneNumber(getterNumber);
            if(getter != null)
            {
                if(getter.CurrentSmartphone != null)
                {
                    getter.CurrentSmartphone.SmartphoneMessages.Add(smartphoneMessage);
                }
            }

            //Save();
            RoleplayContext ctx = Singleton.GetDatabaseInstance();
            using (UnitOfWork unit = new UnitOfWork(ctx))
            {
                unit.SmartphoneMessageRepository.Add(smartphoneMessage);
            }
        }
    }
}
