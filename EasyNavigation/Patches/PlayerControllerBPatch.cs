using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
using UnityEngine.InputSystem;

[HarmonyPatch(typeof(PlayerControllerB))]
internal class PlayerControllerBPatch {
	private static GameObject trailObject;
	private static GameObject guiObject;
	private static TrailRenderer tr;
	private static ManualLogSource mls;

	public static bool isPermanentTrailEnabled = false;
	public static bool showPermanentTrailGUI = false;
	private static float debounceTime = 0.5f;
	private static float lastToggleTime = 0f;

	public static bool showOutsideTrailGUI = false;
	public static bool isOutsideTrailEnabled = false;
	public static bool playerInsideHangarShip = false;
	private static bool lastFramePlayerInsideFactory = false;

	public static bool trailEnabled;

	[HarmonyPatch("Awake")]
	[HarmonyPostfix]
	private static async void SpawnTrailRenderer() {
		trailEnabled = EasyNavigation.trailEnabled.Value;
		if (!trailEnabled) return;
		await WaitSeconds();

		mls = BepInEx.Logging.Logger.CreateLogSource(EasyNavigation.modGUID);

		if (trailObject == null) {
			trailObject = new GameObject("TrailObject");
			trailObject.transform.parent = GameNetworkManager.Instance.localPlayerController.thisPlayerModel.transform;
			trailObject.transform.localPosition = Vector3.zero;
			trailObject.transform.localPosition = new Vector3(22.28f, 0.16f, -11.6f);

			tr = trailObject.AddComponent<TrailRenderer>();
			Material trailMaterial = tr.GetComponent<Renderer>().material;
			trailMaterial.EnableKeyword("_EMISSION");
			trailMaterial.shader = Shader.Find("HDRP/Lit");

			Dictionary<string, Color> namedColors = new Dictionary<string, Color> {
				{"black", Color.black},
				{"blue", Color.blue},
				{"clear", Color.clear},
				{"cyan", Color.cyan},
				{"gray", Color.gray},
				{"green", Color.green},
				{"grey", Color.grey},
				{"magenta", Color.magenta},
				{"red", Color.red},
				{"white", Color.white},
				{"yellow", Color.yellow}
			};

			Color color;
			if (!namedColors.TryGetValue(EasyNavigation.trailColor.Value.ToLower(), out color)) {
				ColorUtility.TryParseHtmlString(EasyNavigation.trailColor.Value, out color);
			}

			mls.LogInfo("Color: " + color);
			trailMaterial.SetColor("_BaseColor", color);
			trailMaterial.SetColor("_EmissiveColor", color);

			trailMaterial.SetFloat("_EmissiveIntensity", 4.0f);
			trailMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
			tr.startWidth = EasyNavigation.trailWidth.Value;
			tr.endWidth = EasyNavigation.trailWidth.Value;
			tr.time = EasyNavigation.trailDuration.Value;

			mls.LogInfo("EasyNavigation has loaded the TrailRenderer!");
		}

		if (guiObject == null) {
			guiObject = new GameObject("GUIObject");
			guiObject.AddComponent<GUIHandler.HandleGUI>();

			mls.LogInfo("EasyNavigation has loaded the GUIObject!");
		}
	}

	[HarmonyPatch("Update")]
	[HarmonyPostfix]
	private static async void UpdateLineRenderer() {
		if (!trailEnabled) return;
		await WaitSeconds();

		string permanentTrailBindingKey = EasyNavigation.permanentTrailBinding.Value;
		bool validPermanentTrailBindingKey = Enum.TryParse<Key>(permanentTrailBindingKey, true, out Key permanentTrailBindingUserKey);

		string outsideTrailBindingKey = EasyNavigation.outsideTrailBinding.Value;
		bool validOutsideTrailBindingKey = Enum.TryParse<Key>(outsideTrailBindingKey, true, out Key outsideTrailBindingUserKey);

		if (Time.time - lastToggleTime > debounceTime) {
			if (validPermanentTrailBindingKey && Keyboard.current[permanentTrailBindingUserKey].wasReleasedThisFrame) {
				isPermanentTrailEnabled = !isPermanentTrailEnabled;
				lastToggleTime = Time.time;
				showPermanentTrailGUI = true;
			} else {
				showPermanentTrailGUI = false;
			}

			if (validOutsideTrailBindingKey && Keyboard.current[outsideTrailBindingUserKey].wasReleasedThisFrame) {
				isOutsideTrailEnabled = !isOutsideTrailEnabled;
				lastToggleTime = Time.time;
				showOutsideTrailGUI = true;
			} else {
				showOutsideTrailGUI = false;
			}
		}

		string clearTrailBindingKey = EasyNavigation.clearTrailBinding.Value;
		bool validClearTrailBindingKey = Enum.TryParse<Key>(clearTrailBindingKey, true, out Key clearTrailBindingUserKey);

		bool playerInsideFactory = GameNetworkManager.Instance.localPlayerController.isInsideFactory;
		playerInsideHangarShip = GameNetworkManager.Instance.localPlayerController.isInHangarShipRoom;

		if (validClearTrailBindingKey && Keyboard.current[clearTrailBindingUserKey].wasPressedThisFrame) {
			tr.Clear();
		}

		if (playerInsideHangarShip && !isPermanentTrailEnabled) {
			tr.Clear();
		}

		// Clear the trail if entering or exiting the factory and permanent trail is not enabled
		if (!isPermanentTrailEnabled) {
			if (playerInsideFactory != lastFramePlayerInsideFactory) {
				tr.Clear();
			}
		}

		// Update the lastFramePlayerInsideFactory
		lastFramePlayerInsideFactory = playerInsideFactory;

		// Handling trail visibility based on conditions
		if (playerInsideFactory) {
			trailObject.SetActive(true); // Trail is always active inside the factory
		} else {
			if (isOutsideTrailEnabled && !playerInsideHangarShip) {
				trailObject.SetActive(true);
			} else {
				trailObject.SetActive(false);
			}
		}

		// Additional case: Both bools are false
		if (!isPermanentTrailEnabled && !isOutsideTrailEnabled && !playerInsideFactory) {
			trailObject.SetActive(false); // Deactivate the trail outside the factory
			tr.Clear();
		}
	}

	private static async Task WaitSeconds() {
		await Task.Delay(TimeSpan.FromSeconds(3.0));
	}
}
