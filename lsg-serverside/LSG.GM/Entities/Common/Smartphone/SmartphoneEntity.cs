//using LSG.DAL.Database.Models.ItemModels;
//using LSG.DAL.Database.Models.SmartphoneModels;
//using LSG.GM.Entities.Core;
//using LSG.GM.Entities.Core.Item;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace LSG.GM.Entities.Common.Smartphone
//{
//    public class SmartphoneEntity
//    {
//        public ItemModel ItemModel { get; set; }
//        public CharacterEntity SmartphoneOwner { get; set; }

//        public int SmartphoneMemory { get; set; }

//        public List<SmartphoneMessageModel> SmartphoneMessages => ItemModel.SmartphoneMessages;
//        public List<Smartphone>


//        public SmartphoneEntity(ItemModel itemModel, CharacterEntity smartphoneOwner)
//        {
//            ItemModel = itemModel;
//            SmartphoneOwner = smartphoneOwner;
//        }


//        public void Start()
//        {
//            SmartphoneOwner.CurrentSmartphone = this;
//        }

//        public void Stop()
//        {
//            SmartphoneOwner.CurrentSmartphone = null;
//        }

//        public void ShowWindow()
//        {
//            if(SmartphoneOwner.CurrentSmartphone != null)
//            {

//            }
//        }
//    }
//}
