using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Markel.Pricing.Service.Infrastructure.Extensions
{
    /// <summary>
    /// Represents a set of useful extension methods for string.
    /// </summary>
    public static partial class Extension
    {
        #region Constants

        private const string EMPTY_STRING_VALUE = "";
        private const char LIST_DELIMITER = '|';
        private const char DICTIONARY_DELIMITER = '=';

        #endregion Constants

        #region Collections

        /// <summary>
        /// Splits string using the split list and dictionary delimeters and returns a generic Dictionary<string, string>.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="lDelimiter">The list delimiter.</param>
        /// <param name="dDelimiter">The dictionary delimiter.</param>
        /// <returns></returns>
        public static Dictionary<string, string> SplitToDictionary(this string obj, char lDelimiter = LIST_DELIMITER, char dDelimiter = DICTIONARY_DELIMITER)
        {
            var dict = new Dictionary<string, string>();

            foreach (var items in obj.SplitToList(lDelimiter))
            {
                var dictItems = items.SplitToList(dDelimiter);
                if (dictItems.Count == 2 && dictItems.All(l => !string.IsNullOrEmpty(l)))
                {
                    dict.Add(dictItems[0], dictItems[1]);
                }
            }
            return dict;
        }

        /// <summary>
        /// Splits string using the specified delimeter and returns a generic List<string>.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="lDelimiter">The list delimiter.</param>
        /// <returns></returns>
        public static List<string> SplitToList(this string obj, char lDelimiter = LIST_DELIMITER)
        {
            if (string.IsNullOrEmpty(obj))
                return new List<string>();

            return obj.Split(lDelimiter).ToList();
        }

        public static List<string> GetRegExGroupNameOrder(string regexExpression, string[] groupNames)
        {
            string[] regexSeperators = { @"(?<", @">(" };
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(regexExpression) && groupNames != null)
            {
                foreach (string s in regexExpression.Split(regexSeperators, StringSplitOptions.None))
                {
                    if (groupNames.Contains(s))
                        list.Add(s);
                }
            }
            return list;
        }

        public static List<string> GetRegExGroupByName(IEnumerable<string> collection, string regexExpression, string groupName)
        {
            List<string> groupValues = new List<string>();

            if (collection != null && collection.Count() != 0)
            {
                Regex re = new Regex(regexExpression, RegexOptions.IgnoreCase);
                foreach (string str in collection)
                {
                    if (string.IsNullOrWhiteSpace(str)) continue;
                    Match match = re.Match(str);
                    if (match.Captures.Count == 1)
                    {
                        // Get regex group vaules
                        for (int gIdx = 0; gIdx < match.Groups.Count; gIdx++)
                        {
                            if (groupName == re.GetGroupNames()[gIdx])
                                groupValues.Add(match.Groups[gIdx].Value);
                        }
                    }
                }
            }
            return groupValues;
        }

        #endregion Collections

        #region Strings

        /// <summary>
        /// Retrieves the current value of the string object, or the specified default value.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns></returns>
        public static string GetValueOrDefault(this string obj, string defaultValue = EMPTY_STRING_VALUE)
        {
            if (string.IsNullOrWhiteSpace(obj))
                return defaultValue;

            return obj;
        }

        /// <summary>
        /// Returns the current value of a string after empty string are removed. If empty, null is returned.
        /// </summary>
        /// <param name="value">String Value</param>
        /// <param name="defaultValue">Default Value</param>
        /// <returns>Trimed value ot Default Value</returns>
        public static string TrimValue(this string value, string defaultValue = EMPTY_STRING_VALUE)
        {
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            return value.Trim();
        }

        /// <summary>
        /// Generates the next name based on the source matching the regex. The source is returned if no match is found.
        /// The output string is built in the order of the group names in the regex. The group name are processed as follows:
        ///     num    = If found the number will be incremented; otherwise, if the num group is not found and AppendNumberIfNotExist 
        ///              is true, then a number will be appened to the generated name.
        ///     suffix = If the suffix exist (st,nd,rd,th,), then a new suffix will be generated based on the incremented number 
        ///              from the num group.
        ///     prefix = Seperator for parts of the generated name.
        ///     name   = Constant string portion of the generated name.
        ///     option = If found, will be dropped from the newly generated name.
        ///     param[0|1|2|3] = The params, if found, will be replaced with the parameter of the same index in the parameters variable.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="regexExpression">The regex expression.</param>
        /// <param name="alwaysIncrementNumber">if set to <c>true</c> the "num" group will always be incremented even if it is not alresy used in the existing names.</param>
        /// <param name="appendNumberIfNotExist">if set to <c>true</c> [append number if not exist].</param>
        /// <param name="existingNames">The list of existing names which the newly generated name cannot duplicate.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public static string GenerateNextName(this string source, string regexExpression, bool alwaysIncrementNumber = false, bool appendNumberIfNotExist = false, IEnumerable<string> existingNames = null, IEnumerable<string> parameters = null)
        {
            string outputString = source;
            string[] groupNames = { "num", "suffix", "prefix", "name", "option", "param0", "param1", "param2", "param3" };
            NameValueCollection regExGroupValues = new NameValueCollection();
            bool foundNumber = false;
            int numberGroupNameIndex = -1;
            string nextNumber = string.Empty;

            Regex re = new Regex(regexExpression, RegexOptions.IgnoreCase); //ETM_REMOVE: | RegexOptions.IgnorePatternWhitespace);
            Match match = re.Match(source);
            if (match.Captures.Count == 1)
            {
                // Get regex group vaules
                for (int gIdx = 0; gIdx < match.Groups.Count; gIdx++)
                {
                    if (groupNames.Contains(re.GetGroupNames()[gIdx]))
                        regExGroupValues.Add(re.GetGroupNames()[gIdx], match.Groups[gIdx].Value);
                }

                // Get existing numbera from existing names
                var existingNumbers = GetRegExGroupByName(existingNames, regexExpression, "num").Select(g => { int n = 0; return (!string.IsNullOrEmpty(g.Trim()) && int.TryParse(g, out n)) ? n : 0; });

                // Increment counter
                if (regExGroupValues.AllKeys.Contains("num"))
                {
                    foundNumber = true;
                    if (!string.IsNullOrEmpty(regExGroupValues["num"].Trim()))
                    {
                        var currentNumber = Convert.ToInt32(regExGroupValues["num"].Trim());
                        if (alwaysIncrementNumber == true)
                            currentNumber++;
                        while (existingNumbers.Contains(currentNumber))
                            currentNumber++;
                        regExGroupValues["num"] = currentNumber.ToString();
                    }
                    else if (appendNumberIfNotExist)
                    {
                        // Increment max number from existing names
                        regExGroupValues["num"] = ((existingNumbers.Count() != 0) ? existingNumbers.Max() + 1 : 1).ToString();
                    }
                }
                // Update sufix
                if (regExGroupValues.AllKeys.Contains("suffix") && !string.IsNullOrEmpty(regExGroupValues["suffix"].Trim()))
                    regExGroupValues["suffix"] = regExGroupValues["num"].GetNumberSuffix();
                // Fixup prefix because RegEx parser is not returning it
                if (regExGroupValues.AllKeys.Contains("prefix") && regExGroupValues["prefix"].Length == 0)
                {
                    //ETM_FIX: Need to fix this to return the prefix string from the regex expression
                    regExGroupValues["prefix"] = GetRegExGroupValue(regexExpression, "prefix");
                }
                // Remove option
                if (regExGroupValues.AllKeys.Contains("option"))
                    regExGroupValues["option"] = string.Empty;
                //Replace parameters
                if (parameters != null)
                {
                    for (int index = 0; index < parameters.Count(); index++)
                    {
                        var paramName = string.Format("param{0}", index);
                        if (regExGroupValues.AllKeys.Contains(paramName))
                            regExGroupValues[paramName] = parameters.ElementAt(index);
                    }
                }

                // If num group not found and using num in the expression, then get next number from existing names
                if (!foundNumber && appendNumberIfNotExist)
                {
                    List<string> groups = GetRegExGroupNameOrder(regexExpression, groupNames);
                    if (groups.Contains("num"))
                    {
                        // Increment max number from existing names
                        nextNumber = ((existingNumbers.Count() != 0) ? existingNumbers.Max() + 1 : 1).ToString();
                        numberGroupNameIndex = groups.IndexOf("num");
                    }
                }

                // Build new name so that number can be inserted
                outputString = string.Empty;
                for (int index = 0; index < regExGroupValues.Keys.Count; index++)
                {
                    if (index == numberGroupNameIndex)
                        outputString += nextNumber;
                    outputString += regExGroupValues[index];
                }
            }

            if (existingNames != null && existingNames.Count() != 0 && existingNames.Select(n => n.ToUpperInvariant()).Contains(outputString.ToUpperInvariant()))
            {
                int layerSuffixId = 0;
                // Get next number suffix
                foreach (string name in existingNames)
                {
                    int number = 0;
                    string[] split2 = name.Split('_');
                    if (split2.Length > 1)
                        int.TryParse(split2[split2.Length - 1], out number);
                    if (number > layerSuffixId)
                        layerSuffixId = number;
                }
                outputString = string.Format("{0}_{1}", outputString, layerSuffixId + 1);
            }

            return outputString;
        }

        /// <summary>
        /// Return the suffix (st, nd, rd, th) for the specified number.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static string GetNumberSuffix(this string obj)
        {
            int number = 0;
            string suffix = string.Empty;
            if (Int32.TryParse(obj.Trim(), out number))
            {
                var ones = number % 10;
                var tens = Math.Floor(number / 10f) % 10;
                if (tens == 1)
                    suffix = "th";
                else switch (ones)
                    {
                        case 1: suffix = "st"; break;
                        case 2: suffix = "nd"; break;
                        case 3: suffix = "rd"; break;
                        default: suffix = "th"; break;
                    }
            }
            return suffix;
        }

        public static string ReplaceValues(this string text, IDictionary<string, string> replacementValues)
        {
            if (string.IsNullOrEmpty(text)) return text;

            foreach (string valueName in replacementValues.Keys)
            {
                string replacementValue = replacementValues[valueName];
                if (string.IsNullOrEmpty(replacementValue))
                {
                    replacementValue = null;
                }

                text = text.Replace(valueName, replacementValue);
            }

            return text;
        }

        public static string RemoveSpecialCharacters(this string text, params char[] validCharacters)
        {
            if (string.IsNullOrEmpty(text)) return text;

            StringBuilder sb = new StringBuilder();
            foreach (char c in text)
            {
                bool isValidCharacter = (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
                if (isValidCharacter || (validCharacters != null && validCharacters.Contains(c)))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string Truncate(this string value, int length)
        {
            if (string.IsNullOrEmpty(value)) return value;

            if (value.Length < length) length = value.Length;
            return value.Substring(0, length);
        }

        public static string GetUniqueName(this IEnumerable<string> names, string newName)
        {
            int nameId = 0;

            foreach (var name in names.Where(n => n.StartsWith(newName)))
            {
                int number = 0;
                var nameParts = name.Replace(newName, "").Split('_');
                if (nameParts.Length == 1 && name.Equals(newName))
                {
                    nameId++;
                }
                else if (nameParts.Length == 2 && int.TryParse(nameParts[1], out number))
                {
                    if (nameId < number)
                    {
                        nameId = number;
                    }
                }
            }

            if (nameId > 0) return string.Format("{0}_{1}", newName, nameId + 1);
            return newName;
        }

        public static string Pluralize<T>(this string word, IEnumerable<T> array)
        {
            if (string.IsNullOrEmpty(word) || array == null) return word;
            string extension = array.Count() > 1 ? "s" : String.Empty;
            return $"{word}{extension}";
        }

        public static string LowerCaseFirstCharacter(this string value)
        {
            return Char.ToLowerInvariant(value[0]) + value.Substring(1);
        }

        public static string SafeReplace(this string input, string find, string replace, bool matchWholeWord = true, bool ignoreCase = false)
        {
            string textToFind = matchWholeWord ? string.Format(@"(?<!\S){0}(?!\S)", Regex.Escape(find)) : find;
            return ignoreCase ? Regex.Replace(input, textToFind, replace, RegexOptions.IgnoreCase) : Regex.Replace(input, textToFind, replace);
        }

        public static string Join(this IEnumerable<string> values, string separator)
        {
            return string.Join(separator, values);
        }

        public static string Last(this string value, char separator = LIST_DELIMITER)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            return value.Split(separator).LastOrDefault();
        }

        public static string GetRegExGroupValue(string regexExpression, string groupName)
        {
            string[] regexSeperators = { @"(?<", @">(" };
            string value = string.Empty;
            if (!string.IsNullOrEmpty(regexExpression) && groupName != null)
            {
                bool processNext = false;
                foreach (string s in regexExpression.Split(regexSeperators, StringSplitOptions.None))
                {
                    if (processNext == true)
                    {
                        string[] groupSeperators = { @"(", @"+", @"?", @"\", @")" };
                        value = string.Join(string.Empty, s.Split(groupSeperators, StringSplitOptions.None));
                        break;
                    }
                    if (s == groupName)
                        processNext = true;
                }
            }
            return value;
        }

        #endregion Strings

        #region Booleans

        public static bool HasValue(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(value.Trim())) return false;

            return true;
        }

        public static bool IsInteger(this string value)
        {
            int output;
            return int.TryParse(value, out output);
        }

        public static bool IsNumeric(this string value)
        {
            float output;
            return float.TryParse(value, out output);
        }

        public static bool ToBoolean(this string value)
        {
            bool flag;
            if (Boolean.TryParse(value, out flag)) return flag;

            throw new Exception(string.Format("Unable to parse '{0}'.", value == null ? "<null>" : value));
        }

        #endregion Booleans
    }
}
