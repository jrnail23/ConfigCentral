namespace ConfigCentral
{
    public static class StringExtensions
    {
        //[NotNull] private static readonly Regex NumericCharacterMatcher = new Regex(@"[\D]", RegexOptions.Compiled);

        [AssertionMethod]
        public static bool IsNullOrEmpty([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [CanBeNull] this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        [AssertionMethod]
        public static bool IsNullOrWhiteSpace([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [CanBeNull] this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        
        [NotNull]
        [StringFormatMethod("format")]
        public static string FormatWith([CanBeNull] this string format, [NotNull] params object[] values)
        {
            return format.IsNullOrEmpty() ? string.Empty : string.Format(format, values);
        }

        //public static bool IsIntegerString([CanBeNull] this string value)
        //{
        //    int intValue;
        //    return !value.IsNullOrEmpty() && int.TryParse(value, out intValue);
        //}

        //public static bool IsDateTimeString([CanBeNull] this string value)
        //{
        //    DateTime parsedDate;
        //    return !value.IsNullOrEmpty() && DateTime.TryParse(value, out parsedDate);
        //}

        //[NotNull]
        //public static string NullSafeTrim([CanBeNull] this string value, [CanBeNull] params char[] charsToTrim)
        //{
        //    return (value ?? string.Empty).Trim(charsToTrim);
        //}

        //public static bool ContainsAnyOf([NotNull] this string source, [NotNull] IEnumerable<string> substrings, StringComparison comparer = StringComparison.Ordinal)
        //{
        //    return substrings.Any(substring => source.Contains(substring, comparer));
        //}

        //[NotNull]
        //public static string TrimSpaces([CanBeNull] this string value)
        //{
        //    return value.IsNullOrEmpty()
        //               ? string.Empty
        //               : string.Join(" ", value.Trim().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries));
        //}

        //[NotNull]
        //public static string ToNullSafeString([CanBeNull] this object value)
        //{
        //    return value.ToNullSafeString(string.Empty);
        //}

        //[NotNull]
        //public static string ToNullSafeString([CanBeNull] this object value,[NotNull] string valueIfNull)
        //{
        //    valueIfNull.EnforceArgumentNotNull("valueIfNull");

        //    return value == null ? valueIfNull : value.ToString();
        //}

        //[NotNull]
        //public static T ToEnum<T>([CanBeNull]this string value)
        //{
        //    if (value.IsNullOrWhiteSpace()) return default(T);

        //    return (T)(Enum.Parse(typeof(T), value, ignoreCase: true));
        //}

        //[NotNull]
        //public static string RemoveNonNumericCharacters([CanBeNull] this string value)
        //{
        //    return value.IsNullOrEmpty() ? string.Empty : NumericCharacterMatcher.Replace(value, string.Empty);
        //}

        //[NotNull]
        //public static string RemoveNonAlphabeticCharacters([CanBeNull] this string value)
        //{
        //    return value.IsNullOrEmpty() ? string.Empty : new string(value.ToCharArray().Where(char.IsLetter).ToArray());
        //}

        //[NotNull]
        //public static string SplitPascalCase([NotNull] this string value)
        //{
        //    value.EnforceArgumentNotNull("value");

        //    return Regex.Replace(value, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        //}

        //public static bool Contains([NotNull] this string source, [NotNull] string value, StringComparison comparison)
        //{
        //    source.EnforceArgumentNotNull("source");
        //    value.EnforceArgumentStringNotNullOrEmpty("value");

        //    return source.IndexOf(value, comparison) >= 0;
        //}

        //[NotNull]
        //public static string ToProperCase([NotNull] this string source)
        //{
        //    source.EnforceArgumentNotNull("source");

        //    return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(source.ToLower());
        //}
    }
}