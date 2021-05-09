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
        public CultureInfo OldCulture { get; private set; }
        public CultureInfo CurrentCulture { get; private set; }

        /// <summary>
        /// Instantiates the CultureChanger object
        /// </summary>
        public CultureChanger()
        {
            OldCulture = Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        /// Instantiates the CultureChanger object and changes the current culture to the <paramref
        /// name="newCulture" />
        /// </summary>
        /// <exception cref="ArgumentNullException"> </exception>
        public CultureChanger(CultureInfo newCulture)
        {
            OldCulture = Thread.CurrentThread.CurrentCulture;
            ChangeCulture(newCulture);
        }

        /// <summary>
        /// Changes the current thread's culture to the <paramref name="newCulture" />
        /// </summary>
        /// <param name="newCulture"> </param>
        /// <exception cref="ArgumentNullException"> </exception>
        public void ChangeCulture(CultureInfo newCulture)
        {
            if (newCulture == null)
                throw new ArgumentNullException(nameof(newCulture));

            CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentCulture = newCulture;
        }

        public void Dispose()
        {
            Thread.CurrentThread.CurrentCulture = OldCulture;
        }
    }
}