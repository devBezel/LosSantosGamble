using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using LSG.GM.Core.Streamers.ObjectStreamer;
using LSG.GM.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace LSG.GM.Entities.Core.Item.Scripts
{
    public class ObjectScript : IScript
    {
        [ClientEvent("item:cancelUseObject")]
        public void CancelObjectUse(IPlayer player, int itemID)
        {
            ItemEntity itemToDelete = player.GetAccountEntity().characterEntity.ItemsInUse.FirstOrDefault(item => item.Id == itemID);
            player.GetAccountEntity().characterEntity.ItemsInUse.Remove(itemToDelete);
            
        }

        [ClientEvent("item:createWorldObject")]
        public void CreateWorldItemObject(IPlayer player, uint objectHash, string positionJson, string rotationJson, int itemID)
        {

            Vector3 position = JsonConvert.DeserializeObject<Vector3>(positionJson);
            Vector3 rotation = JsonConvert.DeserializeObject<Vector3>(rotationJson);

            DynamicObject dynamicObject = ObjectStreamer.CreateDynamicObject(objectHash, position, rotation, player.Dimension, null, true, null, null, null, null, true, 275);

            dynamicObject.SetData("dynamicObject:ownerAccountId", player.GetAccountEntity().DbModel.Id);

            Alt.Log($"ID OBIEKTU STWORZONEGO: {dynamicObject.Id}");
            EntityHelper.Add(dynamicObject);

            player.SendSuccessNotify("Postawiono obiekt pomyślnie", "Twój obiekt został postawiony");
            player.Emit("item:synchronizateWorldObjectWithClient", dynamicObject.Id, itemID);
        }

        [ClientEvent("item:removeWorldObject")]
        public void RemoveWorldItemObject(IPlayer player, uint entityId, int itemID)
        {
            CharacterEntity sender = player.GetAccountEntity().characterEntity;

            DynamicObject dynamicObject = EntityHelper.GetById(entityId);
            dynamicObject.TryGetData("dynamicObject:ownerAccountId", out int dynamicObjectOwnerId);

            Alt.Log($"Account ID: ${dynamicObjectOwnerId}");

            CharacterEntity dynamicObjectOwner = PlayerExtenstion.GetPlayerByAccountId(dynamicObjectOwnerId).characterEntity;

            if (sender != dynamicObjectOwner)
            {
                player.SendErrorNotify("Wystąpił bląd", "Nie jesteś właścicielem tego obiektu");
                return;
            }

            ItemEntity itemToDelete = player.GetAccountEntity().characterEntity.ItemsInUse.FirstOrDefault(item => item.Id == itemID);
            player.GetAccountEntity().characterEntity.ItemsInUse.Remove(itemToDelete);

            EntityHelper.Remove(dynamicObject);
            dynamicObject.Destroy();

        }

        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public void RemoveWorldItemObjectDisconnect(IPlayer player, string reason)
        {
            //TODO: Dorobic usuwanie tymczasowych obiektow z mapy po wyjsciu
            //player.GetAccountEntity().characterEntity.ItemsInUse.Where()
        }

        [Command("createobj")]
        public void CreateObjectTestCMD(IPlayer player, string name)
        {
            ObjectStreamer.CreateDynamicObject(name, new Vector3(player.Position.X, player.Position.Y, player.Position.Z), new Vector3(0, 0, 0), 0);
            player.SendSuccessNotify("Utworzono obiekt", name);
        }
    }
}
