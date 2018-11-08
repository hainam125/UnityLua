using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour {
	private LuaEnvironment lua;
	[SerializeField]
	private GameObject buttonParent;
	[SerializeField]
	private Text button1Text;
	[SerializeField]
	private Text button2Text;

	private void Start() {
		lua = FindObjectOfType<LuaEnvironment>();
		buttonParent.SetActive(false);
	}

	public void ButtonClicked(int index) {
		Debug.Log("Button Clicked: " + index);
		lua.LuaGameState.ButtonSelected = index + 1;
		buttonParent.SetActive(false);
		lua.AdvanceScript();
	}

	public void ShowButtons(string btn1Text, string btn2Text) {
		buttonParent.SetActive(true);
		button1Text.text = btn1Text;
		button2Text.text = btn2Text;
	}

	public bool AreButtonVisible(){
		return buttonParent.gameObject.activeSelf;
	}
}
