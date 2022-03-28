using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VehicleItemRestricted.Types
{
    public class SettingsForItem
    {
        [XmlArray]
        public List<int> BlackList;

        public bool IsInBlackList(ushort idItem)
        {
            foreach(int id in BlackList)
            {
                if(id == idItem)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
