using BepInEx.Configuration;
using GameNetcodeStuff;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace GUIHandler {
    public class HandleGUI : MonoBehaviour {
		private void OnGUI() {
			string permanentTrailText;

			switch (PlayerControllerBPatch.isPermanentTrailEnabled) {
				case true:
					permanentTrailText = "Permanent Trail Enabled!";
					break;
				case false:
					permanentTrailText = "Permanent Trail Disabled!";
					break;
				default:
					permanentTrailText = "Permanent Trail Unknown";
					break;
			}

			if (PlayerControllerBPatch.showGUI) {
				GUI.Box(new Rect((Screen.width - 175) / 2, ((Screen.height - 30) / 2) / 8.4f, 175, 30), permanentTrailText);
			} else {
				return;
			}
		}
	}
}
