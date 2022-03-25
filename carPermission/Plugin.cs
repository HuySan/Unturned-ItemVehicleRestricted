using Rocket.Core.Plugins;
using System;
using SDG.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Rocket.Core;
using Rocket.API.Collections;

namespace vehiclePermission
{
    public class Plugin : RocketPlugin<Configuration>
    {
        protected override void Load()
        {
            VehicleManager.onEnterVehicleRequested += EnterVehicle;
            Rocket.Core.Logging.Logger.Log("Plugin has been loaded!");
        }

        public override TranslationList DefaultTranslations => new TranslationList()
        {
            { "no_access_vehicle", "You can't get in that vehicle ({0})" },
        };


        private void EnterVehicle(Player player, InteractableVehicle vehicle, ref bool shouldAllow)
        {
            UnturnedPlayer uplayer = UnturnedPlayer.FromPlayer(player);
            if (!(GetIdInGroup(uplayer, vehicle.id)))
            {
                UnturnedChat.Say(uplayer, Translate("no_access_vehicle", vehicle.asset.name), UnityEngine.Color.red);
                shouldAllow = false;
            }
        }

        public  bool  GetIdInGroup(UnturnedPlayer uPlayer, int vehicleId)
        {
            foreach(var group in Configuration.Instance.CanSeatOnTheTransport)
            {
                foreach (var permGroup in R.Permissions.GetGroups(uPlayer, false))
                {
                    try
                    {
                        if (group.idGroup.ToLower() != permGroup.Id.ToLower()) continue;

                        if (group.IsInWhiteList(vehicleId)) return true;
                    }
                    catch(Exception ex)
                    {
                        Rocket.Core.Logging.Logger.LogException(ex, "Exception in GetIdInGroup()");
                    }
                }
            }
            return false;
        }
      

        protected override void Unload()
        {
            Rocket.Core.Logging.Logger.Log("Plugin has been unloaded!");
            VehicleManager.onEnterVehicleRequested -= EnterVehicle;            
        }
    }
}
