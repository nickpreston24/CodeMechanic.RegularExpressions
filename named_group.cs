// using System.Reflection;
// using System.Text;
// using System.Text.RegularExpressions;
// using CodeMechanic.Types;
//
// namespace CodeMechanic.RegularExpressions;
//
// public class named_group
// {
//     public Quantifier quantifier { get; set; } = new Quantifier(string.Empty, QuantifierType.None);
//
//     public bool is_plural =>
//         quantifier.Type is QuantifierType.Greedy or QuantifierType.Lazy or QuantifierType.OneOrMore
//             or QuantifierType.ZeroOrMore ||
//         (quantifier.Type == QuantifierType.Range && quantifier.Max > 0 || quantifier.Min > 0);
//
//     public void Deconstruct(out string propertyName, out string groupPattern)
//     {
//         propertyName = property_name;
//         groupPattern = group_pattern;
//     }
//
//     public string quantifier_raw { get; set; } = String.Empty;
//     [CanToString(false)] public int level { get; set; } = -1;
//     public string property_name { get; set; } = string.Empty;
//     public string group_pattern { get; set; } = string.Empty;
//
//     public Regex regex => new Regex(group_pattern,
//         RegexOptions.Compiled | RegexOptions.Multiline |
//         RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
//
//     private static PropertyInfo[] props = [];
//
//     public override string ToString()
//     {
//         var type = GetType();
//         if (props?.Length == 0)
//             props = typeof(named_group).GetProperties(BindingFlags.Instance | BindingFlags.Public);
//
//         var sb = new StringBuilder();
//
//         sb.AppendLine($"--- :: GROUP '{property_name}' :: ---");
//
//         foreach (var prop in props)
//         {
//             var attr = prop.GetCustomAttribute<CanToStringAttribute>();
//
//             if (attr != null)
//             {
//                 if (!attr.Enabled)
//                     continue;
//
//                 if (!string.IsNullOrWhiteSpace(attr.ConditionMethod))
//                 {
//                     var method = type.GetMethod(
//                         attr.ConditionMethod,
//                         BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
//
//                     if (method == null)
//                         continue;
//
//                     var result = method.Invoke(this, null);
//
//                     if (result is not bool shouldPrint || !shouldPrint)
//                         continue;
//                 }
//             }
//
//             var value = prop.GetValue(this);
//             sb.AppendLine($"{prop.Name}={value}");
//         }
//
//         sb.AppendLine("--- :::: ---");
//
//         return sb.ToString();
//     }
// }
//
// [AttributeUsage(AttributeTargets.Property)]
// public sealed class CanToStringAttribute : Attribute
// {
//     public bool Enabled { get; }
//     public string? ConditionMethod { get; }
//
//     public CanToStringAttribute(bool enabled = true)
//     {
//         Enabled = enabled;
//     }
//
//     public CanToStringAttribute(string conditionMethod)
//     {
//         Enabled = true;
//         ConditionMethod = conditionMethod;
//     }
//
//     public CanToStringAttribute(bool enabled, string conditionMethod)
//     {
//         Enabled = enabled;
//         ConditionMethod = conditionMethod;
//     }
// }
//
// public static class QuantifierExtensions
// {
//     private static readonly Regex _rangeRegex =
//         new(@"\{(?<min>\d*)(,(?<max>\d*)?)?\}\??",
//             RegexOptions.Compiled);
//
//     public static Quantifier ParseQuantifier(this string raw, bool debug = false)
//     {
//         if (raw.IsEmpty())
//             return new Quantifier(String.Empty, QuantifierType.None);
//         if (debug) Console.WriteLine($"raw :>> {raw}");
//
//         raw = raw.Trim();
//
//         bool isLazy = raw.Length == 2 && raw.EndsWith("?");
//         if (isLazy)
//             raw = raw[..^1]; // remove trailing '?'
//         if (debug) Console.WriteLine($"raw after :>> {raw}");
//
//         return raw switch
//         {
//             "+" => new Quantifier(raw, QuantifierType.OneOrMore, 1, int.MaxValue, isLazy),
//             "?" => new Quantifier(raw, QuantifierType.ZeroOrOne, 0, 1, isLazy),
//             "*" => new Quantifier(raw, QuantifierType.ZeroOrMore, 0, int.MaxValue, isLazy),
//             _ => ParseRange(raw, isLazy)
//         };
//     }
//
//     private static Quantifier ParseRange(string raw, bool isLazy)
//     {
//         var match = _rangeRegex.Match(raw.Trim());
//
//         var is_a_range = !match.Success;
//         Console.WriteLine($"is a range? {is_a_range}");
//         if (is_a_range)
//             return new Quantifier(raw, QuantifierType.None);
//
//         var minGroup = match.Groups["min"].Value;
//         var maxGroup = match.Groups["max"].Value;
//
//         int min = string.IsNullOrEmpty(minGroup) ? 0 : int.Parse(minGroup);
//         int max;
//
//         if (!match.Groups["max"].Success)
//         {
//             // "{m}"
//             max = min;
//         }
//         else if (string.IsNullOrEmpty(maxGroup))
//         {
//             // "{m,}"
//             max = int.MaxValue;
//         }
//         else
//         {
//             // "{m,n}"
//             max = int.Parse(maxGroup);
//         }
//
//         return new Quantifier(raw,
//             QuantifierType.Range,
//             Min: min,
//             Max: max,
//             IsLazy: isLazy
//         );
//     }
// }
//
// public readonly record struct Quantifier(
//     string type,
//     QuantifierType Type,
//     int? Min = null,
//     int? Max = null,
//     bool IsLazy = false);
//
// public enum QuantifierType
// {
//     None,
//     OneOrMore,
//     ZeroOrMore,
//     ZeroOrOne,
//     Greedy,
//     Lazy,
//     Range
// }
