using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UtilityExtensions.Core.Configurations.Attributes
{
    public class BooleanConfigProperty : ConfigPropertyAttribute
    {
        public override Type type => typeof(bool);

        public BooleanConfigProperty(string @default, string newName = null) : base(@default, newName)
        {
        }

        public override object ConvertFromString(string s) => (s == "1" || s?.ToLower() == "true");

        public override string ConvertToString(object o) => o is bool value ? value ? "1" : "0" : @default;
    }
}