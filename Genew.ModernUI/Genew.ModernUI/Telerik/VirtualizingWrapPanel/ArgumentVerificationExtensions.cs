using System;
using System.Globalization;

namespace ModernUI.ExtendedToolkit
{
    internal static class ArgumentVerificationExtensions
    {
        public static void TestNotNull(this object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void TestNotEmptyString(this string parameter, string parameterName)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                throw new ArgumentException(string.Format("The parameter '{0}' should not be empty string.", parameterName), parameterName);
            }
        }
    }
}
