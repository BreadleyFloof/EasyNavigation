using System;
using System.Threading.Tasks;
using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;
using UnityEngine.InputSystem;

[HarmonyPatch(typeof(PlayerControllerB))]
internal class PlayerControllerBPatch {
	private static GameObject trailObject;
	private static TrailRenderer tr;
	private static ManualLogSource mls;

	private static bool isPermanentTrailEnabled = false;
	private static float debounceTime = 0.5f;
	private static float lastToggleTime = 0f;

	[HarmonyPatch("Awake")]
	[HarmonyPostfix]
	private static async void SpawnTrailRenderer() {
		await WaitSeconds();
		mls = BepInEx.Logging.Logger.CreateLogSource("BREADFPV.EasyNavigation");
		if (trailObject == null) {
			trailObject = new GameObject("TrailObject");
			trailObject.transform.parent = GameNetworkManager.Instance.localPlayerController.thisPlayerModel.transform;
			trailObject.transform.localPosition = Vector3.zero;
			trailObject.transform.localPosition = new Vector3(22.28f, 0.16f, -11.6f);
			tr = trailObject.AddComponent<TrailRenderer>();
			tr.startColor = Color.white;
			tr.endColor = Color.green;
			tr.startWidth = 0.1f;
			tr.endWidth = 0.1f;
			tr.time = float.PositiveInfinity;
			mls.LogInfo("EasyNavigation has loaded the TrailRenderer!");
		}
	}

	[HarmonyPatch("Update")]
	[HarmonyPostfix]
	private static void UpdateLineRenderer() {
		if (Time.time - lastToggleTime > debounceTime && Keyboard.current.oKey.wasReleasedThisFrame) {
			isPermanentTrailEnabled = !isPermanentTrailEnabled;
			lastToggleTime = Time.time;
			Debug.Log("isPermanentTrailEnabled: " + isPermanentTrailEnabled);
		}
		if (GameNetworkManager.Instance.localPlayerController.isInsideFactory) {
			trailObject.SetActive(true);
			if (Keyboard.current.iKey.wasReleasedThisFrame) {
				tr.Clear();
			}
		} else {
			if (!isPermanentTrailEnabled) {
				tr.Clear();
			}
			trailObject.SetActive(false);
		}
	}

	private static async Task WaitSeconds() {
		await Task.Delay(TimeSpan.FromSeconds(3.0));
	}
}
