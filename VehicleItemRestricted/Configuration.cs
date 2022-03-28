using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleItemRestricted.Types;
using System.Xml.Serialization;
namespace VehicleItemRestricted
{
    public class Configuration : IRocketPluginConfiguration
    {
        public List<SettingsForVehicle> CanSeatOnTheTransport;
        public SettingsForItem settingsForItem = new SettingsForItem();

        public bool ignoreAdmins;
        public void LoadDefaults()
        {
            ignoreAdmins = true;

            CanSeatOnTheTransport = new List<SettingsForVehicle>
            {
                new SettingsForVehicle
                {
                    idGroup = "default",
                    WhiteList = new List<int>
                    {
                        1,
                        123,
                    }
                },
                new SettingsForVehicle
                {
                    idGroup = "police",
                    WhiteList = new List<int>
                    {
                        33,
                        106,
                        108,
                    }
                },
                new SettingsForVehicle
                {
                    idGroup = "fireman",
                    WhiteList = new List<int>
                    {
                        34,
                    }
                },
            };
 
            settingsForItem.BlackList = new List<int>
            {
                1041,
                1043,
                1300,
                1394,
            };
        }
    }
}
