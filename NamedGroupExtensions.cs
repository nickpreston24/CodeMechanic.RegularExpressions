// using System.Text.RegularExpressions;
// using CodeMechanic.Diagnostics;
// using CodeMechanic.Types;
//
// namespace CodeMechanic.RegularExpressions;
//
// public static class NamedGroupExtensions
// {
//     private static RegexOptions gmix =
//         RegexOptions.Compiled
//         | RegexOptions.IgnoreCase
//         | RegexOptions.IgnorePatternWhitespace
//         | RegexOptions.Multiline;
//
//     public static Dictionary<named_group, int> GetNestedExtractableProperties(
//         this NamedGroupsRegex nested_named_groups,
//         Dictionary<named_group, int> properties,
//         string model_pattern
//         , int level = 0
//         , bool debug = false)
//     {
//         if (debug)
//             Console.WriteLine($"Examining level {level} ... ");
//         var outer_named_groups =
//             model_pattern.Extract<named_group>(
//                 nested_named_groups.CompiledRegex);
//
//         foreach (var named_group in outer_named_groups)
//         {
//             string updated_pattern = named_group.group_pattern.Trim().RemoveWhiteSpace();
//
//             // if (debug)
//             // {
//             //     Console.WriteLine($"EVIL group pattern {named_group.group_pattern}");
//             //     Console.WriteLine($"GOOD group pattern {updated_pattern}");
//             // }
//
//             named_group.group_pattern = updated_pattern;
//         }
//
//         if (debug)
//             outer_named_groups.Dump(nameof(outer_named_groups));
//
//         level++;
//
//         foreach (var named_group in outer_named_groups)
//         {
//             named_group.level = level;
//             properties.TryAdd(named_group,
//                 level
//             );
//
//             if (named_group.quantifier_raw.NotEmpty())
//             {
//                 if (debug)
//                     Console.WriteLine($"quantifier (raw) : {named_group.quantifier_raw}");
//                 var quantifier = named_group.quantifier_raw.ParseQuantifier();
//                 named_group.quantifier = quantifier;
//             }
//
//             if (NamedGroupsRegex.NamedGroups.CompiledRegex.IsMatch(
//                     named_group.group_pattern))
//             {
//                 var next_layer = GetNestedExtractableProperties(
//                     NamedGroupsRegex.NamedGroups,
//                     properties,
//                     named_group.group_pattern, level);
//
//                 if (debug)
//                     next_layer.Select(x => x.Key)
//                         .Dump(nameof(next_layer) + $" {level}");
//
//                 // foreach (var layer in properties)
//                 // {
//                 //     properties.TryAdd(layer.Key, level);
//                 // }
//
//                 // return properties;
//             }
//         }
//
//         return properties;
//     }
// }
