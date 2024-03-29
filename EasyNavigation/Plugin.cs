﻿using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

[BepInPlugin(modGUID, modName, modVersion)]
public class EasyNavigation : BaseUnityPlugin {
	public const string modGUID = "BREADFPV.EasyNavigation";
	private const string modName = "EasyNavigation";
	private const string modVersion = "0.3.0";

	public static ConfigEntry<bool> trailEnabled;
	public static ConfigEntry<float> trailDuration;
	public static ConfigEntry<string> clearTrailBinding;
	public static ConfigEntry<string> permanentTrailBinding;
	public static ConfigEntry<string> outsideTrailBinding;
	public static ConfigEntry<float> trailWidth;
	public static ConfigEntry<string> trailColor;

	private readonly Harmony harmony = new Harmony(modGUID);

	private static EasyNavigation Instance;

	internal ManualLogSource mls;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		}
		mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
		mls.LogInfo("EasyNavigation has been loaded!");
		harmony.PatchAll(typeof(EasyNavigation));
		harmony.PatchAll(typeof(PlayerControllerBPatch));

		trailEnabled = Config.Bind("General", "Trail Enabled", true, "Change whether the trail is enabled or disabled!");
		trailDuration = Config.Bind("General", "Trail Duration", 240f, "Adjust how long the trail lasts for!");
		clearTrailBinding = Config.Bind("General", "Clear Trail Binding", "i", "Adjust what key is set for clearing the trail!");
		permanentTrailBinding = Config.Bind("General", "Permanent Trail Binding", "o", "Adjust what key is set for enabling or disabling the permanent trail bool!");
		outsideTrailBinding = Config.Bind("General", "Outside Trail Binding", "p", "Adjust what key is set for enabling or disabling the outside trail bool!");
		trailWidth = Config.Bind("General", "Trail Width", 0.1f, "Adjust the width for the trail!");
		trailColor = Config.Bind("General", "Trail Color", "magenta", "Adjust what color is used for the trail!");
	}
}
