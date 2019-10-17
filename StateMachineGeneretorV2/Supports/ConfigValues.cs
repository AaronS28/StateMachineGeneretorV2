using System.Reflection;

namespace StateMachineGeneretorV2.Supports
{
    public static class ConfigValues
    {
        public static string SolutionName { get; set; }
        public static string ScriptLocation { get; set; }
        public static string DestinyPath { get; set; }
        public static string OriginPath { get; set; }
        public static Assembly Assembly { get; set; }
    }
}
