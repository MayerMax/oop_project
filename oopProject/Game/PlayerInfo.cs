using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace oopProject
{
    public abstract class PlayerInfo
    {
        private Dictionary<string,string> attributes;

        public PlayerInfo(Dictionary<string, string> attributes) {
            this.attributes = attributes;
        } 

        public string this[string attributeName] {
            get {
                return attributes[attributeName];
            }
        }

        public List<string> GetAttributesDescriptorName() {
            return GetType().GetFields(BindingFlags.Static)
                .Where(field => field.IsInitOnly)
                .Select(field => field.Name)
                .ToList();
        }

        public int ParseAttribute(string attribute) {
            try {
                return int.Parse(this[attribute]);
            }

            catch (FormatException) {
                throw new ArgumentException("Expected numeric string as attribute value");
            }
        }
        
    }
}
