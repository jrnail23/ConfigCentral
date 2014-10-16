using System;
using System.Diagnostics;

namespace ConfigCentral
{
    /// <summary>
    ///     See http://en.wikipedia.org/wiki/Design_by_contract
    /// </summary>
    [DebuggerStepThrough]
    public static class Contract
    {
        [AssertionMethod]
        [NotNull]
        public static T EnforceArgumentNotNull<T>(
            [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [NoEnumeration] [CanBeNull] this T value,
            [NotNull] [InvokerParameterName] string argumentName,
            [CanBeNull] string message = null)
        {
            // ReSharper disable once CompareNonConstrainedGenericWithNull
            if (value == null)
                throw new ArgumentNullException(argumentName, message ?? "value must not be null");
            return value;
        }

        //[AssertionMethod]
        //public static T EnforceArgumentWithinRange<T>(this T value,
        //    T rangeMin,
        //    T rangeMax,
        //    [NotNull] [InvokerParameterName] string argumentName,
        //    [CanBeNull] string message = null) where T : IComparable
        //{
        //    if (value.CompareTo(rangeMin) == -1 || value.CompareTo(rangeMax) == 1)
        //    {
        //        throw new ArgumentOutOfRangeException(argumentName,
        //            value,
        //            message ?? "value should be between {0} and {1}, but was {2}".FormatWith(rangeMin, rangeMax, value));
        //    }

        //    return value;
        //}

        //[AssertionMethod]
        //public static T EnforceArgumentIsLessThan<T>(this T value,
        //    T rangeMax,
        //    [NotNull] [InvokerParameterName] string argumentName,
        //    [CanBeNull] string message = null) where T : IComparable
        //{
        //    if (value.CompareTo(rangeMax) == 1)
        //    {
        //        throw new ArgumentOutOfRangeException(argumentName,
        //            value,
        //            message ?? "value should be less than {0}, but was {1}".FormatWith(rangeMax, value));
        //    }

        //    return value;
        //}

        //[AssertionMethod]
        //public static T EnforceArgumentIsGreaterThan<T>(this T value,
        //    T rangeMin,
        //    [NotNull] [InvokerParameterName] string argumentName,
        //    [CanBeNull] string message = null) where T : IComparable
        //{
        //    if (value.CompareTo(rangeMin) < 1)
        //    {
        //        throw new ArgumentOutOfRangeException(argumentName,
        //            value,
        //            message ?? "value should be greater than {0}, but was {1}".FormatWith(rangeMin, value));
        //    }

        //    return value;
        //}

        //[AssertionMethod]
        //public static T EnforceArgumentIsGreaterThanOrEqualTo<T>(this T value,
        //    T rangeMin,
        //    [NotNull] [InvokerParameterName] string argumentName,
        //    [CanBeNull] string message = null) where T : IComparable
        //{
        //    if (value.CompareTo(rangeMin) <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException(argumentName,
        //            value,
        //            message ?? "value should be greater than or equal to {0}, but was {1}".FormatWith(rangeMin, value));
        //    }

        //    return value;
        //}

        [AssertionMethod]
        [NotNull]
        public static string EnforceArgumentStringNotNullOrEmpty(
            [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [CanBeNull] this string value,
            [NotNull] [InvokerParameterName] string argumentName)
        {

            value.EnforceArgumentNotNull(argumentName);
            if (value.Length == 0)
                throw new ArgumentException("value must not be an empty string", argumentName);
            return value;
        }

        [AssertionMethod]
        [NotNull]
        public static string EnforceArgumentStringNotNullOrWhitespace(
            [AssertionCondition(AssertionConditionType.IS_NOT_NULL)] [CanBeNull] this string value,
            [NotNull] [InvokerParameterName] string argumentName)
        {
            value.EnforceArgumentNotNull(argumentName);
            if (value.IsNullOrWhiteSpace())
                throw new ArgumentException("value must not be whitespace", argumentName);
            return value;
        }

        //[AssertionMethod]
        //public static string EnforceArgumentStringMaxLength([CanBeNull] this string value,
        //    int maxLength,
        //    [NotNull] [InvokerParameterName] string argumentName)
        //{
        //    if (maxLength <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException("maxLength", maxLength, "value must be greater than zero.");
        //    }

        //    if (value.IsNullOrEmpty())
        //    {
        //        return value;
        //    }

        //    if (value.Length > maxLength)
        //    {
        //        throw new ArgumentException(
        //            string.Format("Value must be {0} characters or less.  Actual Length: {1}", maxLength, value.Length),
        //            argumentName);
        //    }

        //    return value;
        //}

        //[AssertionMethod]
        //[NotNull]
        //public static string EnforceArgumentStringFixedLength([NotNull] this string value,
        //    byte fixedLength,
        //    [NotNull] [InvokerParameterName] string argumentName,
        //    [CanBeNull] string message = null)
        //{
        //    if (fixedLength <= 0)
        //    {
        //        throw new ArgumentOutOfRangeException("fixedLength", fixedLength, "value must be greater than zero.");
        //    }

        //    value.EnforceArgumentNotNull("value");

        //    if (value.Length != fixedLength)
        //    {
        //        throw new ArgumentException(
        //            message ??
        //            string.Format("Value must be exactly {0} characters.  Actual Length: {1}. Value: '{2}'",
        //                fixedLength,
        //                value.Length,
        //                value),
        //            argumentName);
        //    }

        //    return value;
        //}

        //[AssertionMethod]
        //[NotNull]
        //public static Match EnforceEntireStringArgumentMatchesPattern([CanBeNull] this string value,
        //    [NotNull] string regExPattern,
        //    [NotNull] [InvokerParameterName] string argumentName,
        //    RegexOptions matchingOptions = RegexOptions.None)
        //{
        //    regExPattern.EnforceArgumentStringNotNullOrEmpty("regExPattern");
        //    argumentName.EnforceArgumentStringNotNullOrEmpty("argumentName");

        //    return
        //        value.EnforceStringArgumentMatchesPattern(new Regex("^{0}$".FormatWith(regExPattern), matchingOptions),
        //            argumentName);
        //}

        //[AssertionMethod]
        //[NotNull]
        //public static Match EnforceStringArgumentMatchesPattern([CanBeNull] this string value,
        //    [NotNull] Regex matcher,
        //    [NotNull] [InvokerParameterName] string argumentName)
        //{
        //    matcher.EnforceArgumentNotNull("matcher");
        //    argumentName.EnforceArgumentStringNotNullOrEmpty("argumentName");

        //    if (value != null)
        //    {
        //        var match = matcher.Match(value);
        //        if (match.Success)
        //        {
        //            return match;
        //        }
        //    }

        //    throw new ArgumentException(
        //        "Value '{0}' does not match the specified format '{1}'".FormatWith(value ?? "(null)", matcher),
        //        argumentName);
        //}

        //[AssertionMethod]
        //public static void ThrowInvalidOperationExceptionIf(
        //    [AssertionCondition(AssertionConditionType.IS_FALSE)] bool condition,
        //    [CanBeNull] Func<string> message = null)
        //{
        //    if (!condition)
        //    {
        //        return;
        //    }

        //    throw new InvalidOperationException((message ?? (() => string.Empty))());
        //}

        //[AssertionMethod]
        //[NotNull]
        //public static T Enforce<T>(this T value,
        //    [NotNull] Func<T, bool> precondition,
        //    [NotNull] [InvokerParameterName] string argumentName,
        //    [CanBeNull] string message = null)
        //{
        //    ThrowArgumentExceptionIf(!precondition(value), argumentName, message);
        //    return value;
        //}

        //[AssertionMethod]
        //[NotNull]
        //public static T Ensure<T>(this T value,
        //    [NotNull] Func<T, bool> precondition,
        //    [CanBeNull] Func<T, string> message = null)
        //{
        //    if (precondition(value))
        //    {
        //        return value;
        //    }

        //    throw new InvalidOperationException((message ?? (val => string.Empty))(value));
        //}

        //[AssertionMethod]
        //[NotNull]
        //public static T EnsureNotNull<T>([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] this T value, [CanBeNull] Func<T, string> message = null) where T : class
        //{
        //    return value.Ensure(x => x != null, message);
        //}

        //[AssertionMethod]
        //public static void ThrowArgumentExceptionIf(
        //    [AssertionCondition(AssertionConditionType.IS_FALSE)] bool condition,
        //    [InvokerParameterName] [NotNull] string paramName,
        //    [CanBeNull] string message = "")
        //{
        //    if (condition)
        //    {
        //        throw new ArgumentException(message, paramName);
        //    }
        //}
    }
}