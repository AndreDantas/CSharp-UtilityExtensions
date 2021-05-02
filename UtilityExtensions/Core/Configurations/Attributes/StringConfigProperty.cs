using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityExtensions.Core.Configurations.Attributes
{
    public class StringConfigProperty : ConfigPropertyAttribute
    {
        public override Type type => typeof(string);

        public StringConfigProperty(string @default, string newName = null) : base(@default, newName)
        {
        }

        public override object ConvertFromString(string s) => s ?? @default;

        public override string ConvertToString(object o) => o?.ToString() ?? @default;
    }
}