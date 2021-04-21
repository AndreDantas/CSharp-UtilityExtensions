using System;
using System.Globalization;
using System.Threading;

namespace UtilityExtensions.Core
{
    /// <summary>
    /// Used to change the current thread's culture to a new culture, reverting it on dispose.
    /// </summary>
    public class CultureChanger : IDisposable
    {
        public CultureInfo oldCulture { get; private set; }
        public CultureInfo currentCulture { get; private set; }

        public CultureChanger()
        {
            oldCulture = Thread.CurrentThread.CurrentCulture;
        }

        public void ChangeCulture(CultureInfo newCulture)
        {
            if (newCulture == null)
                throw new ArgumentNullException("newCulture");
            Thread.CurrentThread.CurrentCulture = currentCulture = newCulture;
        }

        public void Dispose()
        {
            Thread.CurrentThread.CurrentCulture = oldCulture;
        }
    }
}