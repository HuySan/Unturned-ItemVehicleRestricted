using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VehicleItemRestricted.Types
{
    public class SettingsForVehicle
    {
        //XML
        [XmlAttribute]
        public string idGroup;
        [XmlArray]
        public List<int> WhiteList;

        public bool IsInWhiteList(int idVeh)
        {
            foreach(int id in WhiteList)
            {
                if (id == idVeh) return true;//если тачка,в которую хочет сесть наш маслёнок есть в его группе пермижен

            }
            return false;
        }
    }
}
