using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace UtilityExtensions.Core.Validations
{
    public class Validation<T>
    {
        public string name;
        public Func<T, bool> execute;
        public string onFailError;

        public Validation(Func<T, bool> function, string onFailError, string name = "")
        {
            this.execute = function;
            this.onFailError = onFailError;
            this.name = name;
        }
    }
}