using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityExtensions.Core.Configurations.Attributes
{
    public class IntConfigProperty : ConfigPropertyAttribute
    {
        public override Type type => typeof(int);

        public IntConfigProperty(string @default, string newName = null) : base(@default, newName)
        {
        }

        public override object ConvertFromString(string s) => int.TryParse(s, out int result) ? result : int.TryParse(@default, out result) ? result : 0;

        public override string ConvertToString(object o) => o is int value ? value.ToString() : @default;
    }
}