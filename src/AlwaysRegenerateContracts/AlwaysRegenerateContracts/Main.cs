using Harmony;

namespace AlwaysRegenerateContracts
{
    public static class AlwaysRegenerateContracts
    {
        public static void Init(string directory, string settingsJSON)
        {
            AlwaysRegenerateContracts.ModDirectory = directory;
            var harmony = HarmonyInstance.Create("io.github.rajderks.AlwaysRegenerateContracts");
            harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
        }

        internal static string ModDirectory;
    }

}
