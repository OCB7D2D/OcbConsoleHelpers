using HarmonyLib;
using System.Collections.Generic;
using System.IO;

class PersistCmdHistory
{

    static private string FileName => Path.Combine(
        ConsoleHelpers.path, "cmd-history.txt");

    [HarmonyPatch(typeof(GUIWindowConsole))]
    [HarmonyPatch("OnOpen")]
    public class GUIWindowConsoleOnOpen
    {
        public static void Postfix(
            GUIWindowConsole __instance,
            List<string> ___lastCommands,
            ref int ___lastCommandsIdx)
        {
            if (___lastCommands.Count != 0) return;
            if (!File.Exists(FileName)) return;
            foreach (var entry in File.ReadAllLines(FileName))
                if (!string.IsNullOrEmpty(entry))
                    ___lastCommands.Add(entry);
            ___lastCommandsIdx = ___lastCommands.Count;
        }
    }

    [HarmonyPatch(typeof(GUIWindowConsole))]
    [HarmonyPatch("OnClose")]
    public class GUIWindowConsoleOnClose
    {
        public static void Prefix(
            GUIWindowConsole __instance,
            List<string> ___lastCommands)
        {
            File.WriteAllLines(FileName, ___lastCommands);
        }
    }

}
