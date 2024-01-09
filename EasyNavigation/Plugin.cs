using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

[BepInPlugin(modGUID, modName, modVersion)]
public class EasyNavigation : BaseUnityPlugin {
	public const string modGUID = "BREADFPV.EasyNavigation";
	private const string modName = "EasyNavigation";
	private const string modVersion = "0.0.1";

	private readonly Harmony harmony = new Harmony("BREADFPV.EasyNavigation");

	private static EasyNavigation Instance;

	internal ManualLogSource mls;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		}
		mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
		mls.LogInfo((object)"EasyNavigation has been loaded!");
		harmony.PatchAll(typeof(EasyNavigation));
		harmony.PatchAll(typeof(PlayerControllerBPatch));
	}
}
