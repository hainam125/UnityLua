using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuaCommand : MonoBehaviour {
	private static LuaCommand instance;

	[SerializeField]
	private Text uiText;
	private ButtonHandler buttons;

	private void Awake() {
		instance = this;
		buttons = FindObjectOfType<ButtonHandler>();
	}

	public static void SetText(string text) {
		instance.uiText.text = text;
	}

	public static void ShowButtons(string btn1Text, string btn2Text) {
		instance.buttons.ShowButtons(btn1Text, btn2Text);
	}
}
