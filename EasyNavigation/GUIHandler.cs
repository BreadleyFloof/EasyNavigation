using BepInEx.Configuration;
using GameNetcodeStuff;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace GUIHandler {
    public class HandleGUI : MonoBehaviour {
		private void OnGUI() {
			string permanentTrailGUIText;
			string outsideTrailGUIText;

			switch (PlayerControllerBPatch.isPermanentTrailEnabled) {
				case true:
					permanentTrailGUIText = "Permanent Trail Enabled!";
					break;
				case false:
					permanentTrailGUIText = "Permanent Trail Disabled!";
					break;
				default:
					permanentTrailGUIText = "Permanent Trail Unknown";
					break;
			}

			switch (PlayerControllerBPatch.isOutsideTrailEnabled) {
				case true:
					outsideTrailGUIText = "Outside Trail Enabled!";
					break;
				case false:
					outsideTrailGUIText = "Outside Trail Disabled!";
					break;
				default:
					outsideTrailGUIText = "Outside Trail Unknown";
					break;
			}

			if (PlayerControllerBPatch.showPermanentTrailGUI) {
				GUI.Box(new Rect((Screen.width - 175) / 2, ((Screen.height - 30) / 2) / 8.4f, 175, 30), permanentTrailGUIText);
			}

			if (PlayerControllerBPatch.showOutsideTrailGUI) {
				GUI.Box(new Rect((Screen.width - 175) / 2, ((Screen.height - 30) / 2) / 8.4f, 175, 30), outsideTrailGUIText);
			}
		}
	}
}
