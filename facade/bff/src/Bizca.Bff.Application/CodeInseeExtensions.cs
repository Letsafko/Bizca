namespace Bizca.Bff.Application
{
    public static class CodeInseeExtensions
    {
        private const int MaxCodeInseeLength = 5;

        internal static bool IsCodeInseeWellFormatted(string codeInsee)
        {
            return IsOnlyDigit(codeInsee) && codeInsee.Length == MaxCodeInseeLength;
        }

        private static bool IsOnlyDigit(string codeInsee)
        {
            foreach (char c in codeInsee)
                if (!char.IsDigit(c))
                    return false;

            return true;
        }
    }
}