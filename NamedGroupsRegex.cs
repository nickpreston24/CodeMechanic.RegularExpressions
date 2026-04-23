// namespace CodeMechanic.RegularExpressions;
//
// public class NamedGroupsRegex : RegexEnumBase
// {
//     public static NamedGroupsRegex NamedGroups = new NamedGroupsRegex(
//         1,
//         nameof(NamedGroups),
//         @"\(\?<(?<property_name>\w+)>(?<group_pattern>(?:(?>[^()]+)|(?<Open>\()|(?<-Open>\)))+(?(Open)(?!)))\)(?<quantifier_raw>\{\d*,?\d*\}|\*\?|\+\?|\+|\*|\?)?",
//         @"https://regex101.com/r/c2Welk/2"
//     );
//
//     protected NamedGroupsRegex(int id, string name, string pattern,
//         string uri = "") : base(id, name, pattern, uri)
//     {
//     }
// }
