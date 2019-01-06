using Harmony;

namespace AlwaysRegenerateContracts
{
    public static class AlwaysRegenerateContracts
    {
        public static void Init(string directory, string settingsJSON)
        {
            var harmony = HarmonyInstance.Create("io.github.rajderks.AlwaysRegenerateContracts");
            harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
        }
    }
}
