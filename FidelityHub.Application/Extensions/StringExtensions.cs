using System.Text.RegularExpressions;

namespace FidelityHub.Application.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveAllNonPrintableCharacters(string target)
        {
            return Regex.Replace(target, @"\p{C}+", string.Empty);
        }
    }
}
