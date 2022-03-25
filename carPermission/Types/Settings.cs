using System.Collections.Generic;
using System.Xml.Serialization;

namespace carPermission.Types
{
    public class Settings
    {
        [XmlAttribute]
        public string idGroup;
        [XmlArray]
        public List<int> WhiteList;

        public bool IsInWhiteList(int idVeh)
        {
            foreach(int id in WhiteList)
            {
                if (id == idVeh) return true;

            }
            return false;
        }
    }
}
