using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Markel.Pricing.Service.Infrastructure.Helpers
{
    /// <summary>
    /// Asserter class for Runtime.
    /// </summary>
    public static class Asserter
    {
        private const string ErrorInvalidRange = "Cannot perform range assertion on ({0}) because minimum value {1} is greater than maximum value {2}.";
        private const string ErrorExactLength = "{0} must be {1} characters in length.";
        private const string ErrorRegexMatch = "{0} value failed to pass the regular expression: {1}";
        private const string ErrorEmptyString = "{0} cannot be empty.";

        #region Public Methods
        
        #region Assert
        /// <summary>
        /// Asserts the truth of a given expression or value
        /// </summary>
        /// <param name="logicalExpression">The expression or value that should be evaluated for truth.</param>
        /// <param name="exception">The exception to throw when the logicalExpression is false.</param>
        public static void AssertAndThrow(bool logicalExpression, Exception exception)
        {
            if (logicalExpression == false)
            {
                throw exception;
            }
        }
            
        /// <summary>
        /// Asserts the truth of a given expression or value
        /// </summary>
        /// <param name="logicalExpression">The expression or value that should be evaluated for truth.</param>
        /// <param name="message">The message of the exception when the logicalExpression is false.</param>
        public static void Assert(bool logicalExpression, string message)
        {
            if (logicalExpression == false)
            {
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Asserts the truth of a given expression or value
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="logicalExpression">The expression or value that should be evaluated for truth.</param>
        /// <param name="actualValue">The actual value.</param>
        /// <param name="message">The message of the exception when the logicalExpression is false.</param>
        public static void Assert(string parameterName, bool logicalExpression, object actualValue, string message)
        {
            if (logicalExpression == false)
            {
                if (actualValue == null)
                {
                    throw (new ArgumentNullException(parameterName, message));
                }
                else
                {
                    throw (new ArgumentOutOfRangeException(parameterName, actualValue.ToString(), message));
                }
            }
        }
        /// <summary>
        /// Asserts the truth of a given expression.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="logicalExpression"></param>
        public static void Assert(string parameterName, bool logicalExpression)
        {
            if (logicalExpression == false)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        #endregion Assert

        #region AssertNotLessThan
        /// <summary>
        /// Reports an error if minimumValue is greater then actualValue.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="minimumValue"></param>
        /// <param name="actualValue"></param>
        public static void AssertNotLessThan(string parameterName, int minimumValue, int actualValue)
        {
            if (actualValue < minimumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Reports an error if minimumValue is greater then actualValue.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="minimumValue"></param>
        /// <param name="actualValue"></param>
        public static void AssertNotLessThan(string parameterName, long minimumValue, long actualValue)
        {
            if (actualValue < minimumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Reports an error if minimumValue is greater then actualValue.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="minimumValue"></param>
        /// <param name="actualValue"></param>
        public static void AssertNotLessThan(string parameterName, decimal minimumValue, decimal actualValue)
        {
            if (actualValue < minimumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Reports an error if minimumValue is greater then actualValue.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="minimumValue"></param>
        /// <param name="actualValue"></param>
        public static void AssertNotLessThan(string parameterName, float minimumValue, float actualValue)
        {
            if (actualValue < minimumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Reports an error if minimumValue is greater then actualValue.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="minimumValue"></param>
        /// <param name="actualValue"></param>
        public static void AssertNotLessThan(string parameterName, double minimumValue, double actualValue)
        {
            if (actualValue < minimumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        #endregion AssertNotLessThan

        #region AssertMaximum
        /// <summary>
        /// Reports an error if maximumValue is less then actualValue.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="maximumValue"></param>
        /// <param name="actualValue"></param>
        public static void AssertMaximum(string parameterName, int maximumValue, int actualValue)
        {
            if (actualValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Reports an error if maximumValue is less then actualValue.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="maximumValue"></param>
        /// <param name="actualValue"></param>
        public static void AssertMaximum(string parameterName, long maximumValue, long actualValue)
        {
            if (actualValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Reports an error if maximumValue is less then actualValue.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="maximumValue"></param>
        /// <param name="actualValue"></param>
        public static void AssertMaximum(string parameterName, decimal maximumValue, decimal actualValue)
        {
            if (actualValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Reports an error if maximumValue is less then actualValue.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="maximumValue"></param>
        /// <param name="actualValue"></param>
        public static void AssertMaximum(string parameterName, float maximumValue, float actualValue)
        {
            if (actualValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Reports an error if maximumValue is less then actualValue.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="maximumValue"></param>
        /// <param name="actualValue"></param>
        public static void AssertMaximum(string parameterName, double maximumValue, double actualValue)
        {
            if (actualValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        #endregion AssertMaximum

        #region AssertRange
        /// <summary>
        /// Asserts the value falls within the specified range.
        /// </summary>
        public static void AssertRange(string parameterName, int minimumValue, int actualValue)
        {
            if (actualValue < minimumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Asserts the value falls within the specified range.
        /// </summary>
        public static void AssertRange(string parameterName, decimal minimumValue, decimal actualValue)
        {
            if (actualValue < minimumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Asserts the value falls within the specified range.
        /// </summary>
        public static void AssertRange(string parameterName, byte minimumValue, byte maximumValue, byte actualValue)
        {
            if (minimumValue > maximumValue)
            {
                string message = String.Format(ErrorInvalidRange,
                                               parameterName,
                                               minimumValue,
                                               maximumValue);
                throw new InvalidOperationException(message);
            }

            if (actualValue < minimumValue || actualValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Asserts the value falls within the specified range.
        /// </summary>
        public static void AssertRange(string parameterName, short minimumValue, short maximumValue, short actualValue)
        {
            if (minimumValue > maximumValue)
            {
                string message = String.Format(ErrorInvalidRange,
                                               parameterName,
                                               minimumValue,
                                               maximumValue);
                throw new InvalidOperationException(message);
            }

            if (actualValue < minimumValue || actualValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Asserts the value falls within the specified range.
        /// </summary>
        public static void AssertRange(string parameterName, int minimumValue, int maximumValue, int actualValue)
        {
            if (minimumValue > maximumValue)
            {
                string message = String.Format(ErrorInvalidRange,
                                               parameterName,
                                               minimumValue,
                                               maximumValue);
                throw new InvalidOperationException(message);
            }

            if (actualValue < minimumValue || actualValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Asserts the value falls within the specified range.
        /// </summary>
        public static void AssertRange(string parameterName, long minimumValue, long maximumValue, long actualValue)
        {
            if (minimumValue > maximumValue)
            {
                string message = String.Format(ErrorInvalidRange,
                                               parameterName,
                                               minimumValue,
                                               maximumValue);
                throw new InvalidOperationException(message);
            }

            if (actualValue < minimumValue || actualValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Asserts the value falls within the specified range.
        /// </summary>
        public static void AssertRange(string parameterName, decimal minimumValue, decimal maximumValue, decimal actualValue)
        {
            if (minimumValue > maximumValue)
            {
                string message = String.Format(ErrorInvalidRange,
                                               parameterName,
                                               minimumValue,
                                               maximumValue);
                throw new InvalidOperationException(message);
            }

            if (actualValue < minimumValue || actualValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Asserts the value falls within the specified range.
        /// </summary>
        public static void AssertRange(string parameterName, double minimumValue, double maximumValue, double actualValue)
        {
            if (minimumValue > maximumValue)
            {
                string message = String.Format(ErrorInvalidRange,
                                               parameterName,
                                               minimumValue,
                                               maximumValue);
                throw new InvalidOperationException(message);
            }

            if (actualValue < minimumValue || actualValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Asserts the value falls within the specified range.
        /// </summary>
        public static void AssertRange(string parameterName, float minimumValue, float maximumValue, float actualValue)
        {
            if (minimumValue > maximumValue)
            {
                string message = String.Format(ErrorInvalidRange,
                                               parameterName,
                                               minimumValue,
                                               maximumValue);
                throw new InvalidOperationException(message);
            }

            if (actualValue < minimumValue || actualValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// Asserts the value falls within the specified range.
        /// </summary>
        public static void AssertRange(string parameterName, DateTime minimumValue, DateTime maximumValue, DateTime actualValue)
        {
            if (minimumValue > maximumValue)
            {
                string message = String.Format(ErrorInvalidRange,
                                               parameterName,
                                               minimumValue,
                                               maximumValue);
                throw new InvalidOperationException(message);
            }

            if (actualValue < minimumValue || actualValue > maximumValue)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }
             
        #endregion AssertRange

        #region AssertArray

        /// <summary>
        /// Asserts an array to ensure the arrayIndex requested isn't greater than the array length and the array index is positive. 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public static void AssertArrayIndex(string parameterName, Array array, int arrayIndex)
        {
            if (arrayIndex > array.Length || arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        #endregion

        #region AssertIsNotNull
        /// <summary>
        /// Asserts a value to ensure it is not null.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public static void AssertIsNotNull(string parameterName, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        #endregion AssertIsNotNull

        #region AssertIsNotNullOrEmptyString

        /// <summary>
        /// Asserts a string to ensure it is not null or consists of white spaces.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public static void AssertIsNotNullOrWhitespace(string parameterName, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            else if (value.Length == 0 || string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(String.Format(ErrorEmptyString, parameterName));
            }
        }

        /// <summary>
        /// Asserts a string to ensure it is not null or empty.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        public static void AssertIsNotNullOrEmptyString(string parameterName, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            else if (value.Length == 0 || string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(String.Format(ErrorEmptyString, parameterName));
            }

        }

        #endregion

        #region AssertIsValidString

        /// <summary>
        /// Returns true/false indicating whether or not the string is valid.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value">The string being evaluated</param>
        /// <param name="requiredLength">The mandatory length of the string.</param>
        /// <param name="isNullValid">Indicates whether NULL values are valid.</param>
        /// <returns>Boolean indicating whether the string is valid</returns>
        public static void AssertIsValidString(string parameterName, string value, int requiredLength, bool isNullValid)
        {
            if (value == null)
            {
                if (isNullValid)
                {
                    return;
                }
                else
                {
                    throw new ArgumentNullException(parameterName);
                }
            }
            else
            {
                string message = String.Format(ErrorExactLength,
                                               parameterName,
                                               requiredLength);
                Assert(parameterName, value.Length == requiredLength, value.Length, message);
            }
        }

        /// <summary>
        /// Returns true/false indicating whether or not the string is valid.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value">The string being evaluated</param>
        /// <param name="minimumLength">The minimum length of the string being evaluated</param>
        /// <param name="maximumLength">The maximum length of the string being evaulated</param>
        /// <param name="isNullValid">Indicates whether NULL values are valid.</param>
        /// <returns>Boolean indicating whether the string is valid</returns>
        public static void AssertIsValidString(string parameterName, string value, int minimumLength, int maximumLength, bool isNullValid)
        {
            if (value == null)
            {
                if (isNullValid)
                {
                    return;
                }
                else
                {
                    throw new ArgumentNullException(parameterName);
                }
            }
            else
            {
                AssertRange(parameterName, minimumLength, maximumLength, value.Length);
            }
        }

        /// <summary>
        /// Returns true/false indicating whether or not the string is valid
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value">The string being evaluated</param>
        /// <param name="regularExpression">The regular expression used to evaluate the string</param>
        /// <param name="minimumLength">The minimum length of the string being evaluated</param>
        /// <param name="maximumLength">The maximum length of the string being evaulated</param>
        /// <param name="isNullValid">Indicates whether NULL values are valid.</param>
        /// <returns>Boolean indicating whether the string is valid</returns>
        public static void AssertIsValidString(string parameterName, string value, string regularExpression, int minimumLength, int maximumLength, bool isNullValid)
        {
            AssertIsValidString(parameterName, value, new Regex(regularExpression), minimumLength, maximumLength, isNullValid);
        }

        /// <summary>
        /// Returns true/false indicating whether or not the string is valid
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value">The string being evaluated</param>
        /// <param name="regex">The System.Text.RegularExpressions.Regex object used to evaluate the string</param>
        /// <param name="minimumLength">The minimum length of the string being evaluated</param>
        /// <param name="maximumLength">The maximum length of the string being evaulated</param>
        /// <param name="isNullValid">Indicates whether NULL values are valid.</param>
        /// <returns>Boolean indicating whether the string is valid</returns>
        public static void AssertIsValidString(string parameterName, string value, Regex regex, int minimumLength, int maximumLength, bool isNullValid)
        {
            AssertIsValidString(parameterName, value, minimumLength, maximumLength, isNullValid);

            if (!regex.IsMatch(value))
            {
                string message = String.Format(ErrorRegexMatch,
                                               parameterName,
                                               regex);
                throw new ArgumentException(message);
            }
        }


        #endregion

        #region AssertEnumValueIsDefined

        /// <summary>
        /// Checks an Enum argument to ensure that its value is defined by the specified Enum type.
        /// </summary>
        /// <param name="parameterName">The name of the argument holding the value.</param>
        /// <param name="enumType">The Enum type the value should correspond to.</param>
        /// <param name="value">The value to check for.</param>
        public static void AssertEnumValueIsDefined(string parameterName, Type enumType, object value)
        {
            AssertIsNotNull("enumType", enumType);
            Assert(enumType.IsEnum, String.Format("{0} is not an Enum", enumType));

            if (Enum.IsDefined(enumType, value) == false)
            {
                throw new ArgumentException(
                    String.Format("The value of the argument {0} provided for the enumeration {1} is invalid.", parameterName, enumType));
            }
        }

        #endregion

        #region AssertIsEmailAddress

        /// <summary>
        /// Returns true/false indicating whether or not the e-mail address is valid
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="emailAddress">The e-mail address being evaluated</param>
        /// <param name="minimumLength">The minimum length of the e-mail address being evaluated</param>
        /// <param name="maximumLength">The maximum length of the e-mail address being evaluated</param>
        /// <param name="isNullValid">Indicates whether NULL values are valid.</param>
        /// <returns>Boolean indicating whether the e-mail address is valid</returns>
        public static void AssertIsEmailAddress(string parameterName, string emailAddress, int minimumLength, int maximumLength, bool isNullValid)
        {
            AssertIsValidString(parameterName, emailAddress, Constants.RegularExpressions.Email, minimumLength, maximumLength, isNullValid);
        }

        #endregion

        #endregion
    }
}
