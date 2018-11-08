using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceInput : MonoBehaviour {
	private LuaEnvironment lua;
	private ButtonHandler buttons;

	private void Start () {
		buttons = FindObjectOfType<ButtonHandler>();
		lua = FindObjectOfType<LuaEnvironment>();
	}
	
	private void Update () {
		if (!buttons.AreButtonVisible() && Input.GetKeyDown(KeyCode.Space)) {
			lua.AdvanceScript();
		}
	}
}
