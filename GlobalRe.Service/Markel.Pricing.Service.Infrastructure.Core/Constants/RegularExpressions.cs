using System.Text.RegularExpressions;

namespace Markel.Pricing.Service.Infrastructure.Constants
{
    /// <summary>
    /// Represents a class to handle common regular expressions.
    /// </summary>
    public static class RegularExpressions
    {
        public static readonly Regex Email = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.Compiled);
        public static readonly Regex HtmlTag = new Regex(@"^<([a-z]+)([^<]+)*(?:>(.*)<\/\1>|\s+\/>)$", RegexOptions.Compiled);
        public static readonly Regex IPAddress = new Regex(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", RegexOptions.Compiled);
        public static readonly Regex Url = new Regex(@"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$", RegexOptions.Compiled);
    }
}