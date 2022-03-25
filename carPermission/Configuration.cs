using Rocket.API;
using System.Collections.Generic;
using carPermission.Types;

namespace vehiclePermission
{
    public class Configuration : IRocketPluginConfiguration
    {
        public List<Settings> CanSeatOnTheTransport;
        public void LoadDefaults()
        {
            CanSeatOnTheTransport = new List<Settings>
            {
                new Settings
                {
                    idGroup = "default",
                    WhiteList = new List<int>
                    {
                        1,
                        123,
                    }
                },
                new Settings
                {
                    idGroup = "police",
                    WhiteList = new List<int>
                    {
                        33,
                        106,
                        108,
                    }
                },
                new Settings
                {
                    idGroup = "fireman",
                    WhiteList = new List<int>
                    {
                        34,
                    }
                },
            };
        }
    }
}
