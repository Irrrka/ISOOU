namespace ISOOU.Common
{
    using System;

    public static class CoreValidator
    {
        public static void EnsureNotNull(object obj, string message = null)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(message);
            }
        }
    }
}