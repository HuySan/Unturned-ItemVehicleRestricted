using Rocket.Core.Plugins;
using System;
using SDG.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Rocket.Core;
using Rocket.API.Collections;
using Rocket.Unturned.Events;
using Rocket.Unturned.Enumerations;

namespace VehicleItemRestricted
{
    public class Plugin : RocketPlugin<Configuration>
    {
        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "no_access_vehicle", "You can't get in that vehicle ({0})" },
            { "no_access_item", "You can't take this item ({0})" },
        };

        protected override void Load()
        {
            // VehicleManager.onSwapSeatRequested += sitVeh3;//когда чел пересаживается на другое сидение
            // VehicleManager.OnToggledVehicleLock += sitVeh4;//Когда чел закрывает тачку
            // VehicleManager.onSiphonVehicleRequested += sitVeh;//когда чел открывает тачку отмычкой
            VehicleManager.onEnterVehicleRequested += EnterVehicle;
            UnturnedPlayerEvents.OnPlayerInventoryAdded += InventoryAdd;
            Rocket.Core.Logging.Logger.Log("Plugin has been loaded!");
        }

        private void InventoryAdd(UnturnedPlayer player, InventoryGroup inventoryGroup, byte inventoryIndex, ItemJar P)
        {
            if (player.IsAdmin && Configuration.Instance.ignoreAdmins)
                return;

            if (Configuration.Instance.settingsForItem.IsInBlackList(P.item.id))
            {
                player.Inventory.removeItem((byte)inventoryGroup, inventoryIndex);
                UnturnedChat.Say(player, Translate("no_access_item", P.item.id), UnityEngine.Color.red);
            } 
        }

//----------------------------------
        private void EnterVehicle(Player player, InteractableVehicle vehicle, ref bool shouldAllow)
        {
            UnturnedPlayer uplayer = UnturnedPlayer.FromPlayer(player);
            
            if (uplayer.IsAdmin && Configuration.Instance.ignoreAdmins)
                return;

            if (!(TryGetVehicle(uplayer, vehicle.id)))
            {
                UnturnedChat.Say(uplayer, Translate("no_access_vehicle", vehicle.asset.name), UnityEngine.Color.red);
                shouldAllow = false;
            }
        }

        public  bool  TryGetVehicle(UnturnedPlayer uPlayer, int vehicleId)
        {
            foreach(var group in Configuration.Instance.CanSeatOnTheTransport)//Проходим по списку CanSeatOnTheTransport 
            {
                foreach (var permGroup in R.Permissions.GetGroups(uPlayer, false))//проходим по всем группас указанные в пермижене для маслёнка(default,fireMan)
                {
                    try
                    {
                        if (group.idGroup.ToLower() != permGroup.Id.ToLower()) continue;//если группы конфига нету в пермижен,то пробуем другую группу

                        if (group.IsInWhiteList(vehicleId)) return true;
                    }
                    catch(Exception ex)
                    {
                        Rocket.Core.Logging.Logger.LogException(ex, "Exception in TryGetVehicle()");
                    }
                }
            }
            return false;
        }
      

        protected override void Unload()
        {
            Rocket.Core.Logging.Logger.Log("Plugin has been unloaded!");
            VehicleManager.onEnterVehicleRequested -= EnterVehicle;
            UnturnedPlayerEvents.OnPlayerInventoryAdded -= InventoryAdd;
        }
    }
}
