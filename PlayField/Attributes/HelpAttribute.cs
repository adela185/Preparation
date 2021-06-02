using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayField.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly, AllowMultiple=true, Inherited = true)]
    public class HelpAttribute : Attribute
    {
        public string Description { get; set; }
        public string Version { get; set; }
        public HelpAttribute(string Description_in)
        {
            this.Description = Description_in;
            this.Version = "No version defined for this class.";
        }
    }
}
