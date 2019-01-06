using BattleTech.UI;
using Harmony;
using System.Reflection;

namespace AlwaysRegenerateContracts
{
    /// <summary>
    /// Patch that bypasses the original code in which is checked if contracts have been generated, and if this is the case, not regenerate new contracts.
    /// <para>Instead this patch always generates new contracts when entering the contracts screen and returns false in order to not execute the original method.</para>
    /// <para>Uses reflection in order to gain references to the private fields/methods of the current instance.</para>
    /// </summary>
    [HarmonyPatch(typeof(SGRoomController_CmdCenter))]
    [HarmonyPatch("StartContractScreen")]
    class SGRoomController_CmdCenter_StartContractScreen_Patch
    {
        /// <summary>
        /// Prefix-patch the instances' method
        /// </summary>
        /// <param name="__instance">Instance of the calling SGRoomController_CmdCenter</param>
        /// <returns>false to prevent original method execution</returns>
        public static bool Prefix(SGRoomController_CmdCenter __instance) {

            // Obtain references to private fields/methods
            SGContractsWidget contractsWidget = (SGContractsWidget)__instance.GetType().GetField("contractsWidget", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
            MethodInfo OnContractsFetched =  __instance.GetType().GetMethod("OnContractsFetched", BindingFlags.NonPublic | BindingFlags.Instance);

            // Set the widget state to be visible, as to not confuse the user
            contractsWidget.SetLoadingVisible();

            // Call the contract generation logic with an anonymous delegate as it's completion callback
            __instance.simState.CurSystem.GenerateInitialContracts(delegate() {
                // Display the widget in loaded state
                contractsWidget.Visible = true;
                // Invoke OnContractsFetched to fill the list with the newly generated contracts
                OnContractsFetched.Invoke(__instance, null);
            });
            
            // Return false to bypass original method
            return false;
        }
    }
}