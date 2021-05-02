using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using UtilityExtensions.Extensions;

namespace UtilityExtensions.Core.Configurations.Attributes
{
    public class DateTimeConfigProperty : ConfigPropertyAttribute
    {
        private string format;

        public DateTimeConfigProperty(string @default, string newName = null, string format = "yyyy-MM-dd HH:mm:ss") : base(@default, newName)
        {
            this.format = format;
        }

        public override Type type => typeof(DateTime);

        public override object ConvertFromString(string s) => DateTime.TryParse(s, out DateTime result) ? result : DateTime.TryParse(@default, out result) ? result : new DateTime();

        public override string ConvertToString(object o) => o is DateTime value ? value.TryToString(format) : @default;
    }
}